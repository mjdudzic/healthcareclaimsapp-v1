using System;
using System.Threading.Tasks;

namespace ClaimsSubmission.Api.Application
{
	public interface IBatchSubmissionFeedbackService
	{
		Task GenerateFeedbackDocument(string batchUri);
	}
}