using McGurkin.ServiceProviders;
using Newtonsoft.Json;

namespace McGurkin.Tools.StructGenerator
{
    internal enum TestTypeKnown
    {
        Success, Error
    }

    [JsonConverter(typeof(TestTypeJsonConverter))]
    public struct TestType
    {
        public static explicit operator TestType(string s)
        {
            return Parse(s);
        }

        public static readonly TestType Empty = new(0, string.Empty, string.Empty);

        public override readonly bool Equals(object? obj)
        {
            if (obj is TestType right)
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

        public static bool operator ==(TestType left, TestType right)
        {
            return left.Name == right.Name;
        }

        public static bool operator !=(TestType left, TestType right)
        {
            return !(left == right);
        }

        public static TestType Parse(int id)
        {
            var returnValue = Parse(id.ToString());
            return returnValue;
        }

        public static TestType Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Parameter s cannot be null or white space.", nameof(s));

            var returnValue = TestTypes.AllKnownTestTypes.Where(
                    st => st.Name.Equals(s, StringComparison.CurrentCultureIgnoreCase)
                    || st.Value.Equals(s, StringComparison.CurrentCultureIgnoreCase)
                    || st.Id.ToString() == s
                ).FirstOrDefault();

            if (returnValue.Id == 0)
                throw new Exception(string.Format("A TestType with a name of '{0}' could not be found.", s));

            return returnValue;
        }

        internal TestType(int id, string name, string value) : this()
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

        public static bool TryParse(string s, out TestType result)
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

    internal static class TestTypesKnown
    {
        private static readonly Dictionary<TestTypeKnown, TestType> _Cache;

        internal static TestType FromKnown(TestTypeKnown knownTestType)
        {
            return _Cache.Where(p => p.Key == knownTestType).FirstOrDefault().Value;
        }

        static TestTypesKnown()
        {
            _Cache = new Dictionary<TestTypeKnown, TestType>
            {
                { TestTypeKnown.Success, new TestType(1, "Successfully completed", "Success") },
                { TestTypeKnown.Error, new TestType(2, "Errors occurred", "Error") }
            };

        }

        internal static void LoadKnown(List<TestType> list)
        {
            ArgumentNullException.ThrowIfNull(list);
            list.Clear();
            foreach (var p in _Cache)
                list.Add(p.Value);
        }
    }

    public sealed class TestTypes
    {
        public static List<TestType> AllKnownTestTypes
        {
            get
            {
                var returnValue = new List<TestType>();
                TestTypesKnown.LoadKnown(returnValue);
                return returnValue;
            }
        }

        public static List<TestType> OrderByValue
        {
            get
            {
                var returnValue = new List<TestType>();
                TestTypesKnown.LoadKnown(returnValue);
                return [.. returnValue.OrderBy(st => st.Value)];
            }
        }

        public static List<TestType> OrderByName
        {
            get
            {
                var returnValue = new List<TestType>();
                TestTypesKnown.LoadKnown(returnValue);
                return [.. returnValue.OrderBy(st => st.Name)];
            }
        }

        public static TestType Success { get { return TestTypesKnown.FromKnown(TestTypeKnown.Success); } }
        public static TestType Error { get { return TestTypesKnown.FromKnown(TestTypeKnown.Error); } }
    }

    public class TestTypeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TestType);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                string? val = reader.Value as string;
                if (!string.IsNullOrWhiteSpace(val))
                {
                    TestType returnValue = (TestType)val;
                    return returnValue;
                }
            }
            throw new JsonSerializationException("Expected a valid string value.");
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (null != value)
            {
                var toWrite = (ResponseType)value;
                writer.WriteValue(toWrite.ToString());
                return;
            }
            throw new JsonSerializationException("Expected a valid object.");
        }
    }
}