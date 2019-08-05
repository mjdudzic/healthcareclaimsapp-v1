using Minio;

namespace HealthcareClaims.Infrastructure.Minio.Interfaces
{
	public interface IMinioClientFactory
	{
		MinioClient GetClient();
	}
}