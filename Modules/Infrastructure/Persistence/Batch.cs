using System;

namespace Infrastructure.Persistence
{
	public class Batch
	{
		public Guid Id { get; set; }
		public string BatchUri { get; set; }
		public string SubmissionFeedbackUri { get; set; }
		public string VettingReportUri { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
