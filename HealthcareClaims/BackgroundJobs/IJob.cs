using System.Threading.Tasks;
using Hangfire;
using Hangfire.Server;

namespace HealthcareClaims.BackgroundJobs
{
	public interface IJob<in T>
	{
		Task Execute(T item, PerformContext context, IJobCancellationToken cancellationToken);
	}
}