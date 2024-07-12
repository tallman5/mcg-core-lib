using McGurkin.Runtime.Caching;
using McGurkin.Runtime.Serialization;
using McGurkin.ServiceProviders;
using McGurkin.Services;
using McGurkin.Tools.StructGenerator;

namespace McGurkin.Test
{
    [TestClass]
    public class UnitTest : BaseTest
    {
        private static readonly IRequest rq = new Request
        {
            Timestamp = DateTimeOffset.Now
        };

        private static readonly IResponse rs = new Response
        {
            ResponseType = (ResponseType)"Success",
            UserMessage = "Sample User Message"
        };

        [TestMethod]
        public void CacheHelperTest()
        {
            var cacheKey = "rsKey";
            CacheHelper.Set(cacheKey, rs, DateTimeOffset.Now.AddSeconds(5));
            var rsFromCache = CacheHelper.Get<Response>(cacheKey);
            Assert.IsNotNull(rsFromCache);
            Thread.Sleep(6000);
            rsFromCache = CacheHelper.Get<Response>(cacheKey);
            Assert.IsNull(rsFromCache);
        }

        [TestMethod]
        public void DigitalSizeTest()
        {
            var sizeA = DigitalSize.FromGigabytes(1);
            var sizeB = DigitalSize.FromMegabytes(1024);
            Assert.AreEqual(sizeA, sizeB);
            var sizeC = sizeA + sizeB;
            Assert.IsTrue(sizeC.Bits == 17179869184);
        }

        //[TestMethod]
        //public void EmailTest()
        //{
        //    var sender = new EmailSender(_configuration, _emailLogger);
        //    var a = sender.SendEmailAsync("joseph@mcgurkin.net", "Testing", "This station is conducting a test.");
        //    a.Wait();
        //}

        [TestMethod]
        public void ResponseFromStringTest()
        {
            var rsString = "{\r\n  \"requestId\": \"1a423b94-d671-49c8-ad9e-35dbc4d65c28\",\r\n  \"responseId\": \"c0e9efc2-0903-4b33-a054-c3f42c54ae5c\",\r\n  \"responseType\": \"Success\",\r\n  \"userMessage\": \"Sample Response\"\r\n}";
            var rsObj = Serializer.FromString<Response>(rsString);
            Assert.IsNotNull(rsObj);
        }

        [TestMethod]
        public void ResponseToStringTest()
        {
            var rsString = Serializer.ToString(rs);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(rsString));
        }

        [TestMethod]
        public void SerializationTest()
        {
            var rqString = Serializer.ToString(rq);
            var rqSerialized = Serializer.FromString<Request>(rqString);
            Assert.AreEqual(rq.Timestamp, rqSerialized.Timestamp);
        }

        [TestMethod]
        public void StructGeneratorTest()
        {
            var settings = new StructGeneratorSettings();
            var structCode = StructGenerator.Generate(settings);
            Assert.IsNotNull(structCode);
        }

        [TestMethod]
        public void StructTest()
        {
            string s = "Warning";
            ResponseType rt = ResponseType.Parse(s);
            Assert.IsNotNull(rt);
        }
    }
}
