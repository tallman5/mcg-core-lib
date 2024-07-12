using McGurkin.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Testing.Platform.Logging;
namespace McGurkin.Test;

[TestClass]
public class BaseTest
{
    protected readonly IConfiguration _configuration;
    protected readonly Microsoft.Extensions.Logging.ILogger<EmailSender> _emailLogger;

    public BaseTest()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets("c9a539bd-0995-4716-8df4-1c858b9381b9");
        _configuration = builder.Build();

        var loggerFactory = new LoggerFactory();
        _emailLogger = loggerFactory.CreateLogger<EmailSender>();
    }

    [TestCleanup]
    public void Tc()
    {
    }
}
