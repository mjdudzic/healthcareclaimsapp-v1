using System;
using System.Threading.Tasks;
using HealthcareClaims.Models;

namespace HealthcareClaims.Services
{
	public interface IBatchSubmissionService
	{
		Task<Guid> ProcessBatch(BatchSubmitModel model);
	}
}