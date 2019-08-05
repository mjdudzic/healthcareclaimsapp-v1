using System;
using System.Threading.Tasks;

namespace HealthcareClaims.Services
{
	public interface IBatchSubmissionFeedbackService
	{
		Task GenerateFeedbackDocument(Guid batchId);
	}
}