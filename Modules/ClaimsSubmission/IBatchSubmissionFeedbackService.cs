using System;
using System.Threading.Tasks;

namespace ClaimsSubmission
{
	public interface IBatchSubmissionFeedbackService
	{
		Task GenerateFeedbackDocument(Guid batchId);
	}
}