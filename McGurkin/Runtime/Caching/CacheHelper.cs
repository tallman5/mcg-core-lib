using McGurkin.Runtime.Serialization;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;

namespace McGurkin.Runtime.Caching;

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

    /// <summary>
    /// Converts a URL into a safe cache key by normalizing and hashing it.
    /// </summary>
    public static string GetSafeCacheKeyFromUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL must not be null or empty", nameof(url));

        // Normalize the URL (you could optionally sort query params here)
        var normalizedUrl = url.Trim().ToLowerInvariant();

        // Hash the normalized URL to keep key short and consistent
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(normalizedUrl));

        // Convert hash to a hex string
        var sb = new StringBuilder();
        foreach (var b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
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
