using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Minio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HealthcareClaimsWithModules.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IOptions<AppConfig> _appConfig;
		private readonly IObjectsStorageServiceConfiguration _objectsStorageServiceConfiguration;

		public ValuesController(
			IConfiguration configuration,
			IOptions<AppConfig> appConfig,
			IObjectsStorageServiceConfiguration objectsStorageServiceConfiguration)
		{
			_configuration = configuration;
			_appConfig = appConfig;
			_objectsStorageServiceConfiguration = objectsStorageServiceConfiguration;
		}
		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[]
			{
				_configuration["ObjectsStorage:Endpoint"],
				_appConfig.Value.ObjectsStorage.Endpoint,
				_objectsStorageServiceConfiguration.Endpoint
			};
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
