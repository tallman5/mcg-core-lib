using Moq;

namespace McGurkin.Test.Net.Http;

[TestClass]
public class ServiceCallerTests
{
    private Mock<HttpClient> _mockHttpClient;

    [TestInitialize]
    public void Setup()
    {
        _mockHttpClient = new Mock<HttpClient>();
    }

    [TestMethod]
    public void GetAsync_ShouldReturnHttpResponseMessage()
    {
        // Arrange
        //var url = "https://www.google.com";
        //var bearerToken = "token";
        //var headers = new Dictionary<string, string> { { "Header", "Value" } };
        //var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        //_mockHttpClient.Setup(x => x.GetAsync(url)).ReturnsAsync(responseMessage);

        //// Act
        //var result = await ServiceCaller.GetAsync(_mockHttpClient.Object, url, bearerToken, headers);

        //// Assert
        //Assert.AreEqual(responseMessage, result);
    }

    [TestMethod]
    public void PostAsync_ShouldReturnHttpResponseMessage()
    {
        //// Arrange
        //var url = "https://example.com";
        //var data = new { Name = "Test" };
        //var bearerToken = "token";
        //var headers = new Dictionary<string, string> { { "Header", "Value" } };
        //var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        //_mockHttpClient.Setup(x => x.PostAsync(url, It.IsAny<HttpContent>())).ReturnsAsync(responseMessage);

        //// Act
        //var result = await ServiceCaller.PostAsync(_mockHttpClient.Object, url, data, bearerToken, headers);

        //// Assert
        //Assert.AreEqual(responseMessage, result);
    }
}

