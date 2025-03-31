using McGurkin.ServiceProviders;

namespace McGurkin.Test.ServiceProviders;

[TestClass]
public class ResponseTypeTests
{
    [TestMethod]
    public void Parse_ShouldReturnCorrectResponseType()
    {
        // Arrange
        var responseTypeString = "Success";

        // Act
        var responseType = ResponseType.Parse(responseTypeString);

        // Assert
        Assert.AreEqual(ResponseTypes.Success, responseType);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Parse_ShouldThrowExceptionForInvalidResponseType()
    {
        // Arrange
        var responseTypeString = "Invalid";

        // Act
        ResponseType.Parse(responseTypeString);
    }

    [TestMethod]
    public void TryParse_ShouldReturnTrueForValidResponseType()
    {
        // Arrange
        var responseTypeString = "Success";

        // Act
        var result = ResponseType.TryParse(responseTypeString, out var responseType);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(ResponseTypes.Success, responseType);
    }

    [TestMethod]
    public void TryParse_ShouldReturnFalseForInvalidResponseType()
    {
        // Arrange
        var responseTypeString = "Invalid";

        // Act
        var result = ResponseType.TryParse(responseTypeString, out var responseType);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(ResponseType.Empty, responseType);
    }
}

