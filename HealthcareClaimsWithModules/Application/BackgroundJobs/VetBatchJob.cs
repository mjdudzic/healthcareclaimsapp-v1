using System.Threading.Tasks;
using ClaimsVetting;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

namespace HealthcareClaimsWithModules.Application.BackgroundJobs
{
	public class VetBatchJob : IJob<BatchJobData>
	{
		private readonly IBatchVettingService _batchVettingService;

		public VetBatchJob(IBatchVettingService batchVettingService)
		{
			_batchVettingService = batchVettingService;
		}

		public async Task Execute(BatchJobData batchJobData, PerformContext context, IJobCancellationToken cancellationToken)
		{
			context.WriteLine($"Generating vetting report for batch {batchJobData.BatchId}");

			await _batchVettingService.VetBatch(batchJobData.BatchId);
		}
	}
}