using System;
using System.Threading.Tasks;
using ClaimsSubmission.Infrastructure.Persistence;
using HealthcareClaims.Models;
using HealthcareClaims.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareClaims.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BatchesController : ControllerBase
	{
		private readonly IBatchSubmissionService _batchSubmissionService;
		private readonly BatchesContext _batchesContext;

		public BatchesController(
			IBatchSubmissionService batchSubmissionService,
			BatchesContext batchesContext)
		{
			_batchSubmissionService = batchSubmissionService;
			_batchesContext = batchesContext;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] BatchSubmitModel model)
		{
			var result = await _batchSubmissionService.ProcessBatch(model);

			return AcceptedAtAction(
				nameof(GetBatchStatus),
				new { id = result },
				null);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBatchStatus(Guid id)
		{
			return Ok(await _batchesContext.Batches.FindAsync(id));
		}
	}
}
