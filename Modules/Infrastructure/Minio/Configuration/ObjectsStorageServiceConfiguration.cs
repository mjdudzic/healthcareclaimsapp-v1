using Infrastructure.Minio.Interfaces;

namespace Infrastructure.Minio.Configuration
{
	public class ObjectsStorageServiceConfiguration : IObjectsStorageServiceConfiguration
	{
		public string Endpoint { get; set; }
		public string AccessKey { get; set; }
		public string SecretKey { get; set; }
	}
}