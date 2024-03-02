using McGurkin.Runtime.Serialization;
using System.Runtime.Caching;

namespace McGurkin.Runtime.Caching
{
    public class CacheHelper
    {
        public static void Clear()
        {
            MemoryCache.Default.Trim(100);
        }

        public static T? Get<T>(string cacheKey)
        {
            string? cachedObject = MemoryCache.Default.Get(cacheKey) as string;
            if (!string.IsNullOrWhiteSpace(cachedObject))
                return Serializer.FromCompressedString<T>(cachedObject);
            return default;
        }

        public static void Remove(string cacheKey)
        {
            MemoryCache.Default.Remove(cacheKey);
        }

        public static void Set(string cacheKey, object o)
        {
            var expiration = DateTimeOffset.Now.AddHours(24);
            Set(cacheKey, o, expiration);
        }

        public static void Set(string cacheKey, object o, DateTimeOffset expiration)
        {
            var deflated = Serializer.ToCompressedString(o);
            MemoryCache.Default.Set(cacheKey, deflated, expiration);
        }
    }
}
