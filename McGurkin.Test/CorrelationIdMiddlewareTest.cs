using McGurkin.Runtime.Serialization;
using McGurkin.ServiceProviders;
using System.Text;

namespace McGurkin.Test;

[TestClass]
public class CorrelationIdMiddlewareTest : BaseTest
{
    [TestMethod]
    public async Task AddCorrelationIdWhenMissing()
    {
        var rq = new Request { };
        StringContent content = new(Serializer.ToString(rq), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/", content);
        response.EnsureSuccessStatusCode();

        var correlationIdExists = response.Headers.TryGetValues(Constants.X_CORRELATION_ID, out var values);
        var returnedCorrelationId = values?.FirstOrDefault();

        Assert.IsTrue(correlationIdExists, "Correlation ID should be added to the response headers.");
        Assert.IsNotNull(values, "Correlation ID values should not be null.");
        Assert.IsTrue(Guid.TryParse(returnedCorrelationId, out _), "Correlation ID should be a valid GUID.");
    }

    [TestMethod]
    public async Task ReplaceBadCorrelationId()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/");
        requestMessage.Headers.Add(Constants.X_CORRELATION_ID, "1234");
        var response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        var correlationIdExists = response.Headers.TryGetValues(Constants.X_CORRELATION_ID, out var values);
        var returnedCorrelationId = values?.FirstOrDefault();

        Assert.IsTrue(correlationIdExists, "Correlation ID should be added to the response headers.");
        Assert.IsNotNull(values, "Correlation ID values should not be null.");
        Assert.IsTrue(Guid.TryParse(returnedCorrelationId, out _), "Correlation ID should be a valid GUID.");
    }

    [TestMethod]
    public async Task ReuseExistingCorrelationId()
    {
        var newGuid = Guid.NewGuid();
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/");
        requestMessage.Headers.Add("X-COrrelation-id", newGuid.ToString());
        var response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        var correlationIdExists = response.Headers.TryGetValues(Constants.X_CORRELATION_ID, out var values);
        var returnedCorrelationId = values?.FirstOrDefault();

        Assert.IsTrue(correlationIdExists, "Correlation ID should be added to the response headers.");
        Assert.IsNotNull(values, "Correlation ID values should not be null.");
        Assert.IsTrue(returnedCorrelationId == newGuid.ToString(), "Correlation ID should be reused.");
    }
}
