using System.Text.Json;
using System.Text.Json.Serialization;

namespace McGurkin.ServiceProviders
{
    internal enum ResponseTypeKnown
    {
        Success, Warning, Error
    }

    [JsonConverter(typeof(ResponseTypeJsonConverter))]
    public struct ResponseType
    {
        public static explicit operator ResponseType(string s)
        {
            return Parse(s);
        }

        public static readonly ResponseType Empty = new(0, string.Empty, string.Empty);

        public override readonly bool Equals(object? obj)
        {
            if (obj is ResponseType right)
            {
                return this == right;
            }
            return false;
        }

        public override readonly int GetHashCode()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                return string.Empty.GetHashCode();
            else
                return this.Name.GetHashCode();
        }

        public static bool operator ==(ResponseType left, ResponseType right)
        {
            return left.Name == right.Name;
        }

        public static bool operator !=(ResponseType left, ResponseType right)
        {
            return !(left == right);
        }

        public static ResponseType Parse(int id)
        {
            var returnValue = Parse(id.ToString());
            return returnValue;
        }

        public static ResponseType Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Parameter s cannot be null or white space.", nameof(s));

            var returnValue = ResponseTypes.AllKnownResponseTypes.Where(
                    st => st.Name.Equals(s, StringComparison.CurrentCultureIgnoreCase)
                    || st.Value.Equals(s, StringComparison.CurrentCultureIgnoreCase)
                    || st.Id.ToString() == s
                ).FirstOrDefault();

            if (returnValue.Id == 0)
                throw new Exception(string.Format("A ResponseType with a name of '{0}' could not be found.", s));

            return returnValue;
        }

        internal ResponseType(int id, string name, string value) : this()
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public string Value { get; internal set; }

        public override readonly string ToString()
        {
            string returnValue = (string.IsNullOrWhiteSpace(Value)) ? "Empty" : Value;
            return returnValue;
        }

        public static bool TryParse(string s, out ResponseType result)
        {
            bool returnValue = true;
            result = Empty;
            try
            {
                if (!string.IsNullOrEmpty(s))
                    result = Parse(s);
            }
            catch { }
            if (result == Empty)
                returnValue = false;
            return returnValue;
        }
    }

    internal static class ResponseTypesKnown
    {
        private static readonly Dictionary<ResponseTypeKnown, ResponseType> _Cache;

        internal static ResponseType FromKnown(ResponseTypeKnown knownResponseType)
        {
            return _Cache.Where(p => p.Key == knownResponseType).FirstOrDefault().Value;
        }

        static ResponseTypesKnown()
        {
            _Cache = new Dictionary<ResponseTypeKnown, ResponseType>
            {
                { ResponseTypeKnown.Success, new ResponseType(1, "Successfully completed", "Success") },
                { ResponseTypeKnown.Warning, new ResponseType(2, "Completed with warnings", "Warning") },
                { ResponseTypeKnown.Error, new ResponseType(3, "Errors occurred", "Error") }
            };

        }

        internal static void LoadKnown(List<ResponseType> list)
        {
            ArgumentNullException.ThrowIfNull(list);
            list.Clear();
            foreach (var p in _Cache)
                list.Add(p.Value);
        }
    }

    public sealed class ResponseTypes
    {
        public static List<ResponseType> AllKnownResponseTypes
        {
            get
            {
                var returnValue = new List<ResponseType>();
                ResponseTypesKnown.LoadKnown(returnValue);
                return returnValue;
            }
        }

        public static List<ResponseType> OrderByValue
        {
            get
            {
                var returnValue = new List<ResponseType>();
                ResponseTypesKnown.LoadKnown(returnValue);
                return [.. returnValue.OrderBy(st => st.Value)];
            }
        }

        public static List<ResponseType> OrderByName
        {
            get
            {
                var returnValue = new List<ResponseType>();
                ResponseTypesKnown.LoadKnown(returnValue);
                return [.. returnValue.OrderBy(st => st.Name)];
            }
        }

        public static ResponseType Success { get { return ResponseTypesKnown.FromKnown(ResponseTypeKnown.Success); } }
        public static ResponseType Warning { get { return ResponseTypesKnown.FromKnown(ResponseTypeKnown.Warning); } }
        public static ResponseType Error { get { return ResponseTypesKnown.FromKnown(ResponseTypeKnown.Error); } }
    }

    public class ResponseTypeJsonConverter : JsonConverter<ResponseType>
    {
        public override ResponseType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string? val = reader.GetString();
                if (!string.IsNullOrWhiteSpace(val))
                {
                    ResponseType returnValue = (ResponseType)val;
                    return returnValue;
                }
            }
            throw new JsonException("Expected a valid string value.");
        }

        public override void Write(Utf8JsonWriter writer, ResponseType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}