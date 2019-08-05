using System;
using System.Threading.Tasks;

namespace HealthcareClaims.Services
{
	public interface IBatchVettingService
	{
		Task VetBatch(Guid batchId);
	}
}