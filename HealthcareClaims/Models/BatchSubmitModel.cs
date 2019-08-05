using System;
using Microsoft.AspNetCore.Http;

namespace HealthcareClaims.Models
{
	public class BatchSubmitModel
	{
		public Guid HealthcareProviderId { get; set; }
		public IFormFile BatchJsonFile { get; set; }
	}
}