namespace Infrastructure.Minio.Model
{
	public class ObjectResult
	{
		public string BucketName { get; set; }
		public string ObjectName { get; set; }
		public string Content { get; set; }
	}
}