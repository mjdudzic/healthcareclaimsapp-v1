using System;
using System.Threading.Tasks;
using HealthcareClaimsWithModules.Models;

namespace HealthcareClaimsWithModules.Application.Services
{
	public interface IBatchSubmissionService
	{
		Task<Guid> ProcessBatch(BatchSubmitModel model);
	}
}