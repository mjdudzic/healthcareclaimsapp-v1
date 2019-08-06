using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ClaimsSubmission.Api.Application;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsSubmission.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubmissionRequestsController : ControllerBase
	{
		private readonly IBatchSubmissionFeedbackService _batchSubmissionFeedbackService;

		public SubmissionRequestsController(IBatchSubmissionFeedbackService batchSubmissionFeedbackService)
		{
			_batchSubmissionFeedbackService = batchSubmissionFeedbackService;
		}

		// POST api/values
		[HttpPost("batchUri")]
		public async Task Post(SubmissionMode submissionMode)
		{
			await _batchSubmissionFeedbackService.GenerateFeedbackDocument(submissionMode.BatchUri);
		}
	}

	public class SubmissionMode
	{
		public string BatchUri { get; set; }
	}
}
