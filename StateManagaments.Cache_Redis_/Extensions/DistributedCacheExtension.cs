using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace StateManagaments.Cache_Redis_.Extensions
{
    public static class DistributedCacheExtension
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordKey, T data, TimeSpan? absoluteExpirationTime = null, TimeSpan? unusedExpirationTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpirationTime ?? TimeSpan.FromMinutes(3);
            options.SlidingExpiration = unusedExpirationTime;
            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(
                key: recordKey,
                value: jsonData,
                options: options
            );

        }
        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordKey)
        {
            var jsonData = await cache.GetStringAsync(recordKey);
            if (jsonData is null) return default(T);
            return JsonSerializer.Deserialize<T>(jsonData);
        }
        public static async Task RemoveRecordAsync(this IDistributedCache cache, string recordKey)
        {
            await cache.RemoveAsync(recordKey);
        }
    }
}