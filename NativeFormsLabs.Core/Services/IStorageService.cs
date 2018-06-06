namespace NativeFormsLabs.Core.Services
{
	using System;
	using System.Threading.Tasks;

	public interface IStorageService
    {
		Task ClearAsync();

		void Shutdown();

		Task<T> GetOrFetchObjectAsync<T>(string key, Func<Task<T>> fetchFunc, DateTimeOffset? absoluteExpiration = null);

		Task SaveAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class;
	}
}
