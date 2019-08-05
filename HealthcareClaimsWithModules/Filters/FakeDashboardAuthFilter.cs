using Hangfire.Dashboard;

namespace HealthcareClaimsWithModules.Filters
{
	public class FakeDashboardAuthFilter : IDashboardAuthorizationFilter
	{
		public bool Authorize(DashboardContext context)
		{
			return true;
		}
	}
}