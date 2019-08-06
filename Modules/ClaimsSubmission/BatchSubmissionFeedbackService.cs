using System;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClaimsSubmission
{
	public class BatchSubmissionFeedbackService : IBatchSubmissionFeedbackService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly BatchesContext _batchesContext;
		private readonly ILogger<BatchSubmissionFeedbackService> _logger;

		public BatchSubmissionFeedbackService(
			IHttpClientFactory httpClientFactory,
			BatchesContext batchesContext,
			ILogger<BatchSubmissionFeedbackService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_batchesContext = batchesContext;
			_logger = logger;
		}

		public async Task GenerateFeedbackDocument(Guid batchId)
		{
			var client = _httpClientFactory.CreateClient();
			var batch = await _batchesContext.Batches.FindAsync(batchId);
			await client.PostAsJsonAsync(
				"http://claimssubmissionapi/submissionrequests",
				new SubmissionMode
			{
				BatchUri = batch.BatchUri
			});
		}
	}

	public class SubmissionMode
	{
		public string BatchUri { get; set; }
	}
}