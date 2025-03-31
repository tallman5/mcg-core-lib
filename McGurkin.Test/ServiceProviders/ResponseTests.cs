using McGurkin.ServiceProviders;

namespace McGurkin.Test.ServiceProviders;

[TestClass]
public class ResponseTests
{
    [TestMethod]
    public void AppendMessage_ShouldAppendExceptionMessage()
    {
        // Arrange
        var response = new Response();
        var exception = new Exception("Test exception");

        // Act
        response.AppendMessage(exception);

        // Assert
        Assert.IsTrue(response.SystemMessage.Contains("Test exception"));
        Assert.AreEqual(ResponseTypes.Error, response.ResponseType);
    }

    [TestMethod]
    public void AppendMessage_ShouldAppendFormattedMessage()
    {
        // Arrange
        var response = new Response();
        var message = "Test message {0}";

        // Act
        response.AppendMessage(message, 1);

        // Assert
        Assert.IsTrue(response.SystemMessage.Contains("Test message 1"));
    }

    [TestMethod]
    public void AppendMessage_ShouldAppendMessage()
    {
        // Arrange
        var response = new Response();
        var message = "Test message";

        // Act
        response.AppendMessage(message);

        // Assert
        Assert.IsTrue(response.SystemMessage.Contains(message));
    }

    [TestMethod]
    public void ResponseT_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var response = new Response<string>();

        // Assert
        Assert.AreEqual(ResponseTypes.Success, response.ResponseType);
        Assert.IsNull(response.SystemMessage);
        Assert.IsNull(response.UserMessage);
        Assert.IsNull(response.Data);
    }
}

