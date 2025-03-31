using McGurkin.Runtime.Caching;
using System.Runtime.Caching;

namespace McGurkin.Test.Runtime.Caching;

[TestClass]
public class CacheHelperTests
{
    [TestMethod]
    public void Clear_ShouldClearCache()
    {
        // Arrange
        CacheHelper.Set("key", "value");

        // Act
        CacheHelper.Clear();

        // Assert
        Assert.IsNull(MemoryCache.Default.Get("key"));
    }

    [TestMethod]
    public void Get_ShouldReturnCachedValue()
    {
        // Arrange
        var value = "value";
        CacheHelper.Set("key", value);

        // Act
        var result = CacheHelper.Get<string>("key");

        // Assert
        Assert.AreEqual(value, result);
    }

    [TestMethod]
    public void Remove_ShouldRemoveCachedValue()
    {
        // Arrange
        CacheHelper.Set("key", "value");

        // Act
        CacheHelper.Remove("key");

        // Assert
        Assert.IsNull(MemoryCache.Default.Get("key"));
    }

    [TestMethod]
    public void Set_ShouldCacheValue()
    {
        // Arrange
        var value = "value";

        // Act
        CacheHelper.Set("key", value);

        // Assert
        Assert.AreEqual(value, CacheHelper.Get<string>("key"));
    }
}

