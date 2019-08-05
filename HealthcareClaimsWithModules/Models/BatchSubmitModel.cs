using System;
using Microsoft.AspNetCore.Http;

namespace HealthcareClaimsWithModules.Models
{
	public class BatchSubmitModel
	{
		public Guid HealthcareProviderId { get; set; }
		public IFormFile BatchJsonFile { get; set; }
	}
}