using McGurkin.ServiceProviders;

namespace McGurkin.Test.ServiceProviders;

[TestClass]
public class RequestTests
{
    [TestMethod]
    public void Request_ShouldInitializeWithCurrentTimestamp()
    {
        // Arrange & Act
        var request = new Request();

        // Assert
        Assert.AreEqual(DateTime.Now.Date, request.Timestamp.Date);
    }

    [TestMethod]
    public void RequestT_ShouldInitializeWithCurrentTimestamp()
    {
        // Arrange & Act
        var request = new Request<string> { Query = "Test" };

        // Assert
        Assert.AreEqual(DateTime.Now.Date, request.Timestamp.Date);
    }
}

