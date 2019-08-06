using System;
using System.Threading.Tasks;
using HealthcareClaimsWithModules.Application.Services;
using HealthcareClaimsWithModules.Models;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HealthcareClaimsWithModules.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BatchesController : ControllerBase
	{
		private readonly IBatchSubmissionService _batchSubmissionService;
		private readonly BatchesContext _batchesContext;
		private readonly ILogger<BatchesController> _logger;

		public BatchesController(
			IBatchSubmissionService batchSubmissionService,
			BatchesContext batchesContext,
			ILogger<BatchesController> logger)
		{
			_batchSubmissionService = batchSubmissionService;
			_batchesContext = batchesContext;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] BatchSubmitModel model)
		{
			_logger.LogInformation("Batch submitted {@model}", model);
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
