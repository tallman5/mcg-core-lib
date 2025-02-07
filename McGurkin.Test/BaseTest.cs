using McGurkin.Net.Http;
using McGurkin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Testing.Platform.Logging;

namespace McGurkin.Test;

[TestClass]
public class BaseTest
{
    protected readonly IConfiguration _configuration;
    protected readonly Microsoft.Extensions.Logging.ILogger<EmailSender> _emailLogger;
    protected readonly IWebHostBuilder _webHostBuilder;
    protected readonly TestServer _testServer;
    protected readonly HttpClient _httpClient;

    public BaseTest()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<McGurkin.Constants>();

        _configuration = builder.Build();

        var loggerFactory = new LoggerFactory();
        _emailLogger = loggerFactory.CreateLogger<EmailSender>();

        _webHostBuilder = new WebHostBuilder()
            .Configure(app =>
            {
                app.UseMiddleware<CorrelationIdMiddleware>();
                app.Run(async context =>
                {
                    var correlationId = context.Response.Headers[Constants.X_CORRELATION_ID].ToString();
                    await context.Response.WriteAsync(correlationId);
                });
            });
        _testServer = new TestServer(_webHostBuilder);
        _httpClient = _testServer.CreateClient();
    }

    [TestCleanup]
    public void TestsDone()
    {
        _httpClient.Dispose();
        _testServer.Dispose();
    }
}
