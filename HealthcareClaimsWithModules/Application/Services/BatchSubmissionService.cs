using System;
using System.Threading.Tasks;
using Hangfire;
using HealthcareClaimsWithModules.Application.BackgroundJobs;
using HealthcareClaimsWithModules.Models;
using Infrastructure.Minio.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace HealthcareClaimsWithModules.Application.Services
{
	public class BatchSubmissionService : IBatchSubmissionService
	{
		private readonly IObjectsStorageService _objectsStorageService;
		private readonly IBackgroundJobClient _backgroundJobClient;
		private readonly BatchesContext _batchesContext;
		private readonly ILogger<BatchSubmissionService> _logger;

		public BatchSubmissionService(
			IObjectsStorageService objectsStorageService,
			IBackgroundJobClient backgroundJobClient,
			BatchesContext batchesContext,
			ILogger<BatchSubmissionService> logger)
		{
			_objectsStorageService = objectsStorageService;
			_backgroundJobClient = backgroundJobClient;
			_batchesContext = batchesContext;
			_logger = logger;
		}

		public async Task<Guid> ProcessBatch(BatchSubmitModel model)
		{
			var id = Guid.NewGuid();

			var batch = new Batch
			{
				Id = id,
				BatchUri = $"HealthcareClaimsV1/{id}.json",
				CreationDate = DateTime.UtcNow
			};

			_batchesContext.Batches.Add(batch);

			using (var stream = model.BatchJsonFile.OpenReadStream())
			{
				await _objectsStorageService.UploadObjectAsync(
					"healthcare-claims-app-v1", 
					batch.BatchUri, 
					stream);
			}

			await _batchesContext.SaveChangesAsync();

			var batchJobData = new BatchJobData
			{
				BatchId = batch.Id
			};

			var submissionJobId = _backgroundJobClient
				.Enqueue<GenerateBatchSubmissionFeedbackDocumentJob>(
					job => job.Execute(batchJobData, null, JobCancellationToken.Null));

			_backgroundJobClient
				.ContinueJobWith<VetBatchJob>(
					submissionJobId, 
					job => job.Execute(batchJobData, null, JobCancellationToken.Null));

			_logger.LogInformation("Batch submission initiated: {Id}", batch.Id);

			return batch.Id;
		}
	}
}