using McGurkin.ComponentModel;
using McGurkin.Runtime.Serialization;
using McGurkin.ServiceProviders;
using McGurkin.Tools.StructGenerator;

namespace McGurkin.Test
{
    [TestClass]
    public class UnitTest
    {
        private static readonly IRequest rq = new Request
        {
            RequestId = Guid.NewGuid(),
            Timestamp = DateTimeOffset.Now
        };
        private static readonly IResponse rs = new Response
        {
            RequestId = Guid.NewGuid(),
            ResponseType = (ResponseType)"Success",
            UserMessage = "Sample Response"
        };

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
            Assert.AreEqual(rq.RequestId, rqSerialized.RequestId);
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
