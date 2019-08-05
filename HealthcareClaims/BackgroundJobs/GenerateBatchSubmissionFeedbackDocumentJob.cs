﻿using System.Threading.Tasks;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using HealthcareClaims.Services;

namespace HealthcareClaims.BackgroundJobs
{
	public class GenerateBatchSubmissionFeedbackDocumentJob : IJob<BatchJobData>
	{
		private readonly IBatchSubmissionFeedbackService _batchSubmissionFeedbackService;

		public GenerateBatchSubmissionFeedbackDocumentJob(
			IBatchSubmissionFeedbackService batchSubmissionFeedbackService)
		{
			_batchSubmissionFeedbackService = batchSubmissionFeedbackService;
		}

		public async Task Execute(BatchJobData batchJobData, PerformContext context, IJobCancellationToken cancellationToken)
		{
			context.WriteLine($"Generating feedback for batch {batchJobData.BatchId}");

			await _batchSubmissionFeedbackService.GenerateFeedbackDocument(batchJobData.BatchId);
		}
	}
}