﻿namespace HealthcareClaims.Infrastructure.Minio.Interfaces
{
	public interface IObjectsStorageServiceConfiguration
	{
		string Endpoint { get; set; }
		string AccessKey { get; set; }
		string SecretKey { get; set; }
	}
}