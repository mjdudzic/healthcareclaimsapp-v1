using Hangfire.Dashboard;

namespace HealthcareClaims.Filters
{
	public class FakeDashboardAuthFilter : IDashboardAuthorizationFilter
	{
		public bool Authorize(DashboardContext context)
		{
			return true;
		}
	}
}