using McGurkin.ServiceProviders;

namespace McGurkin.Test.ServiceProviders;

[TestClass]
public class ResponseTests
{
    [TestMethod]
    public void ResponseT_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var response = new Response<string>();

        // Assert
        Assert.AreEqual(ResponseTypes.Success, response.ResponseType);
        Assert.IsNull(response.Data);
    }
}

