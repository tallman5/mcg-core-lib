using System.Text.Json;

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
                        PropertyNameCaseInsensitive = true,
                    };
                }
                return _Options;
            }
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
