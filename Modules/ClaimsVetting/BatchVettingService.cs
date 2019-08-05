using System;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClaimsVetting
{
	public class BatchVettingService : IBatchVettingService
	{
		private readonly BatchesContext _batchesContext;
		private readonly ILogger<BatchVettingService> _logger;

		public BatchVettingService(
			BatchesContext batchesContext,
			ILogger<BatchVettingService> logger)
		{
			_batchesContext = batchesContext;
			_logger = logger;
		}

		public async Task VetBatch(Guid batchId)
		{
			var batch = await _batchesContext.Batches
				.FirstOrDefaultAsync(i => i.Id == batchId);

			batch.VettingReportUri = batch.BatchUri.Replace(".json", "-vetting-report.json");

			await _batchesContext.SaveChangesAsync();

			_logger.LogInformation("Batch vetting report generated: {Id}", batch.Id);
		}
	}
}