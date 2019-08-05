using Minio;

namespace Infrastructure.Minio.Interfaces
{
	public interface IMinioClientFactory
	{
		MinioClient GetClient();
	}
}