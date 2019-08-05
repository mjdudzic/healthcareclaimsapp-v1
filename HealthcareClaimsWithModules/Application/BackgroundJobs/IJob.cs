using System.Threading.Tasks;
using Hangfire;
using Hangfire.Server;

namespace HealthcareClaimsWithModules.Application.BackgroundJobs
{
	public interface IJob<in T>
	{
		Task Execute(T item, PerformContext context, IJobCancellationToken cancellationToken);
	}
}