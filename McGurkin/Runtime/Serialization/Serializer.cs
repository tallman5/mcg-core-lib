using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace McGurkin.Runtime.Serialization
{
    /// <summary>
    /// The Serializer is simply helper functions for serializing and deserializing objects.
    /// Newtonsoft cis used since the MS functions do not automatically pick up properties 
    /// on derived classes extending base classes.
    /// </summary>
    public class Serializer
    {
        public static T CopyObject<T>(object o)
        {
            var oJson = ToString(o);
            var returnValue = FromString<T>(oJson);
            return returnValue;
        }

        public static object FromString(string s)
        {
            var returnValue = JsonConvert.DeserializeObject(s, Settings);
            if (null == returnValue)
                throw new Exception("An error occurred deserializing");
            return returnValue;
        }

        public static object FromString(string s, JsonSerializerSettings settings)
        {
            var returnValue = JsonConvert.DeserializeObject(s, settings);
            if (null == returnValue)
                throw new Exception("An error occurred deserializing");
            return returnValue;
        }

        public static T FromString<T>(string s)
        {
            return FromString<T>(s, Settings);
        }

        public static T FromString<T>(string s, JsonSerializerSettings settings)
        {
            var returnValue = JsonConvert.DeserializeObject<T>(s, settings);
            if (null == returnValue)
                throw new Exception("An error occurred deserialzing");
            return returnValue;
        }

        private static JsonSerializerSettings? _Settings;
        public static JsonSerializerSettings Settings
        {
            get
            {
                if (null == _Settings)
                {
                    _Settings = new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        },
                        NullValueHandling = NullValueHandling.Ignore,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        Formatting = Formatting.Indented,
                        Converters = new JsonConverter[]
                        {
                            new StringEnumConverter(),
                        },
                    };
                }
                return _Settings;
            }
        }

        public static string ToString(object o)
        {
            return ToString(o, Settings);
        }

        public static string ToString(object o, JsonSerializerSettings settings)
        {
            var returnValue = JsonConvert.SerializeObject(o, settings);
            return returnValue;
        }
    }
}
