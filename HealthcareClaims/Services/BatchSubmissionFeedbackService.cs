﻿using System;
using System.Threading.Tasks;
using ClaimsSubmission.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HealthcareClaims.Services
{
	public class BatchSubmissionFeedbackService : IBatchSubmissionFeedbackService
	{
		private readonly BatchesContext _batchesContext;
		private readonly ILogger<BatchSubmissionFeedbackService> _logger;

		public BatchSubmissionFeedbackService(
			BatchesContext batchesContext,
			ILogger<BatchSubmissionFeedbackService> logger)
		{
			_batchesContext = batchesContext;
			_logger = logger;
		}

		public async Task GenerateFeedbackDocument(Guid batchId)
		{
			var batch = await _batchesContext.Batches
				.FirstOrDefaultAsync(i => i.Id == batchId);

			batch.SubmissionFeedbackUri = batch.BatchUri.Replace(".json", "-feedback.json");

			await _batchesContext.SaveChangesAsync();

			_logger.LogInformation("Feedback document generated for {Id}", batch.Id);
		}
	}
}