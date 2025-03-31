using McGurkin.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Moq;

namespace McGurkin.Test.ApplicationInsights;

[TestClass]
public class UnwantedTelemetryFilterTests
{
    [TestMethod]
    public void Process_ShouldFilterOutHubRequests()
    {
        // Arrange
        var mockNext = new Mock<ITelemetryProcessor>();
        var filter = new UnwantedTelemetryFilter(mockNext.Object);
        var requestTelemetry = new RequestTelemetry { Name = "TestHub" };

        // Act
        filter.Process(requestTelemetry);

        // Assert
        mockNext.Verify(x => x.Process(It.IsAny<ITelemetry>()), Times.Never);
    }

    [TestMethod]
    public void Process_ShouldPassThroughNonHubRequests()
    {
        // Arrange
        var mockNext = new Mock<ITelemetryProcessor>();
        var filter = new UnwantedTelemetryFilter(mockNext.Object);
        var requestTelemetry = new RequestTelemetry { Name = "TestRequest" };

        // Act
        filter.Process(requestTelemetry);

        // Assert
        mockNext.Verify(x => x.Process(It.IsAny<ITelemetry>()), Times.Once);
    }
}

