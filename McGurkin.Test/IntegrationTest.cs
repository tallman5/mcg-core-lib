//namespace McGurkin.Test;

//[TestClass]
//public class IntegrationTest : BaseTest
//{
//    [TestMethod]
//    public async Task EmailTestAsync()
//    {
//        var testTo = _configuration["TestTo"];
//        if (!string.IsNullOrWhiteSpace(testTo))
//        {
//            var esc = EmailSenderConfig.GetEmpty();
//            _configuration.GetSection("EmailSenderConfig").Bind(esc);
//            var es = new EmailSender(esc, _emailLogger);
//            await es.SendEmailAsync(testTo, "Testing", "This station is conducting a test.");
//        }
//    }
//}
