using System;
using System.Threading.Tasks;

namespace ClaimsVetting
{
	public interface IBatchVettingService
	{
		Task VetBatch(Guid batchId);
	}
}