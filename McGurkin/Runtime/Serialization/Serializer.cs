using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace McGurkin.Runtime.Serialization
{
    public class Serializer
    {
        public static T Copy<T>(object o)
        {
            var objJson = ToString(o);
            var returnValue = FromString<T>(objJson);
            return returnValue;
        }

        public static T FromCompressedString<T>(string s)
        {
            byte[] gZipBuffer = Convert.FromBase64String(s);
            string objectString = string.Empty;

            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                objectString = Encoding.UTF8.GetString(buffer);
            }

            T? returnValue = FromString<T>(objectString);
            return returnValue;
        }

        public static T FromString<T>(string s)
        {
            var returnValue = FromString<T>(s, Options);
            return returnValue;
        }

        public static T FromString<T>(string s, JsonSerializerOptions options)
        {
            var returnValue = JsonSerializer.Deserialize<T>(s, options)
                ?? throw new Exception("Could not deserialize string.");
            return returnValue;
        }

        private static JsonSerializerOptions? _Options;
        public static JsonSerializerOptions Options
        {
            get
            {
                if (null == _Options)
                {
                    _Options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        PropertyNameCaseInsensitive = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        Converters = { new JsonStringEnumConverter() }
                    };
                }
                return _Options;
            }
        }

        public static string ToCompressedString(object o)
        {
            string inflatedString = ToString(o);

            byte[] buffer = Encoding.UTF8.GetBytes(inflatedString);
            byte[] gZipBuffer;

            using (var memoryStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gZipStream.Write(buffer, 0, buffer.Length);
                }

                memoryStream.Position = 0;

                var compressedData = new byte[memoryStream.Length];
                memoryStream.Read(compressedData, 0, compressedData.Length);

                gZipBuffer = new byte[compressedData.Length + 4];

                Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            }

            string returnValue = Convert.ToBase64String(gZipBuffer);
            return returnValue;
        }

        public static string ToString(object o)
        {
            return ToString(o, Options);
        }

        public static string ToString(object o, JsonSerializerOptions options)
        {
            var returnValue = JsonSerializer.Serialize(o, options);
            return returnValue;
        }
    }
}
