using McGurkin.Runtime.Serialization;
using McGurkin.ServiceProviders;

namespace McGurkin.Test.Runtime.Serialization;

[TestClass]
public class SerializerTests
{
    [TestMethod]
    public void Copy_ShouldReturnCopyOfObject()
    {
        // Arrange
        var original = new Request
        {
            Timestamp = DateTimeOffset.Now
        };

        // Act
        var copy = Serializer.Copy<Request>(original);

        // Assert
        Assert.AreEqual(original.Timestamp, copy.Timestamp);
    }

    [TestMethod]
    public void FromCompressedString_ShouldReturnDecompressedObject()
    {
        // Arrange
        var original = new Request
        {
            Timestamp = DateTimeOffset.Now
        };

        var compressedString = Serializer.ToCompressedString(original);

        // Act
        var result = Serializer.FromCompressedString<Request>(compressedString);

        // Assert
        Assert.AreEqual(original.Timestamp, result.Timestamp);
    }

    [TestMethod]
    public void FromString_ShouldReturnDeserializedObject()
    {
        // Arrange
        var original = new Request
        {
            Timestamp = DateTimeOffset.Now
        };
        var jsonString = Serializer.ToString(original);

        // Act
        var result = Serializer.FromString<Request>(jsonString);

        // Assert
        Assert.AreEqual(original.Timestamp, result.Timestamp);
    }

    [TestMethod]
    public void ToCompressedString_ShouldReturnCompressedString()
    {
        // Arrange
        var original = new { Name = "Test" };

        // Act
        var result = Serializer.ToCompressedString(original);

        // Assert
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void ToString_ShouldReturnJsonString()
    {
        // Arrange
        var original = new { Name = "Test" };

        // Act
        var result = Serializer.ToString(original);

        // Assert
        Assert.IsNotNull(result);
    }
}

