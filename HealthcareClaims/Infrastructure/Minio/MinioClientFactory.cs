﻿using HealthcareClaims.Infrastructure.Minio.Interfaces;
using Minio;

namespace HealthcareClaims.Infrastructure.Minio
{
	public class MinioClientFactory : IMinioClientFactory
	{
		private readonly IObjectsStorageServiceConfiguration _objectsStorageServiceConfiguration;

		private MinioClient _client;

		public MinioClientFactory(IObjectsStorageServiceConfiguration objectsStorageServiceConfiguration)
		{
			_objectsStorageServiceConfiguration = objectsStorageServiceConfiguration;
		}

		public MinioClient GetClient()
		{
			return _client ?? (_client = new MinioClient(
					_objectsStorageServiceConfiguration.Endpoint,
					_objectsStorageServiceConfiguration.AccessKey,
					_objectsStorageServiceConfiguration.SecretKey));
		}
	}
}
