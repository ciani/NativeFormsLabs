namespace NativeFormsLabs.Core.Services
{
	using Akavache;
	using System;
	using System.Collections.Generic;
	using System.Reactive.Linq;
	using System.Threading.Tasks;

	public class StorageService : IStorageService
	{
		private readonly IBlobCache localBlobCache;

		public StorageService()
		{
			BlobCache.ApplicationName = "MyAppName";
			localBlobCache = BlobCache.LocalMachine;
		}

		public async Task ClearAsync()
		{
			await localBlobCache.InvalidateAll();
		}

		public void Shutdown()
		{
			BlobCache.Shutdown().Wait();
		}

		public async Task<T> GetOrFetchObjectAsync<T>(string key, Func<Task<T>> fetchFunc, DateTimeOffset? absoluteExpiration = default(DateTimeOffset?))
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new InvalidOperationException($"{nameof(key)} cannot be null or white space.");

			if (fetchFunc == null)
				throw new InvalidOperationException($"{nameof(fetchFunc)} cannot be null.");

			try
			{
				return await localBlobCache.GetOrFetchObject(key, fetchFunc, absoluteExpiration);
			}
			catch (KeyNotFoundException e)
			{
				throw new InvalidOperationException($"{nameof(key)} not found: {e.Message}.");
			}
		}

		public async Task SaveAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new InvalidOperationException($"{nameof(key)} cannot be null or white space.");

			if (value == null)
				throw new InvalidOperationException($"{nameof(value)} cannot be null.");

			if (expiration != null && expiration.HasValue)
				await localBlobCache.InsertObject(key, value, expiration.Value);
			else
				await localBlobCache.InsertObject(key, value);
		}
	}
}
