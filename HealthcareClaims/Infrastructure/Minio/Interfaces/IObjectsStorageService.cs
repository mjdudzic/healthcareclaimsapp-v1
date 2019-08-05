using System.IO;
using System.Threading.Tasks;
using HealthcareClaims.Infrastructure.Minio.Model;

namespace HealthcareClaims.Infrastructure.Minio.Interfaces
{
	public interface IObjectsStorageService
	{
		Task<ObjectResult> GetObjectContentAsync(string bucketName, string objectName);
		Task<string> GetObjectPresignedUrlAsync(string bucketName, string objectName, int expirationThresholdInSecs);
		Task UploadObjectAsync(string bucketName, string objectName, Stream objectStream);
	}
}