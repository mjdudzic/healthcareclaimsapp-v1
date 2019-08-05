using HealthcareClaims.Infrastructure.Minio.Interfaces;

namespace HealthcareClaims.Infrastructure.Minio.Configuration
{
	public class ObjectsStorageServiceConfiguration : IObjectsStorageServiceConfiguration
	{
		public string Endpoint { get; set; }
		public string AccessKey { get; set; }
		public string SecretKey { get; set; }
	}
}