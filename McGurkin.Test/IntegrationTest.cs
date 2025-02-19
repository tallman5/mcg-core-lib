using McGurkin.Services;

namespace McGurkin.Test;

[TestClass]
public class IntegrationTest : BaseTest
{
    [TestMethod]
    public async Task EmailTestAsync()
    {
        var testTo = _configuration["TestTo"];
        if (!string.IsNullOrWhiteSpace(testTo))
        {
            var es = new EmailSender(_configuration, _emailLogger);
            await es.SendEmailAsync(testTo, "Testing", "This station is conducting a test.");
        }
    }
}
