using System.Text.Json;
using System.Text.Json.Serialization;

namespace {{Namespace}}
{
    internal enum {{StructSingular}}Known
    {
        {{SingleKnownEnum}}
    }

    [JsonConverter(typeof({{StructSingular}}JsonConverter))]
    public struct {{StructSingular}}
    {
        public static explicit operator {{StructSingular}}(string s)
        {
            return Parse(s);
        }

        public static readonly {{StructSingular}} Empty = new(0, string.Empty, string.Empty);

        public override readonly bool Equals(object? obj)
        {
            if (obj is {{StructSingular}} right)
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

        public static bool operator ==({{StructSingular}} left, {{StructSingular}} right)
        {
            return left.Name == right.Name;
        }

        public static bool operator !=({{StructSingular}} left, {{StructSingular}} right)
        {
            return !(left == right);
        }

        public static {{StructSingular}} Parse(int id)
        {
            var returnValue = Parse(id.ToString());
            return returnValue;
        }

        public static {{StructSingular}} Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Parameter s cannot be null or white space.", nameof(s));

            var returnValue = {{StructPlural}}.AllKnown{{StructSingular}}s.Where(
                    st => st.Name.Equals(s, StringComparison.CurrentCultureIgnoreCase)
                    || st.Value.Equals(s, StringComparison.CurrentCultureIgnoreCase)
                    || st.Id.ToString() == s
                ).FirstOrDefault();

            if (returnValue.Id == 0)
                throw new Exception(string.Format("A {{StructSingular}} with a name of '{0}' could not be found.", s));

            return returnValue;
        }

        internal {{StructSingular}}(int id, string name, string value) : this()
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

        public static bool TryParse(string s, out {{StructSingular}} result)
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

    internal static class {{StructPlural}}Known
    {
        private static readonly Dictionary<{{StructSingular}}Known, {{StructSingular}}> _Cache;

        internal static {{StructSingular}} FromKnown({{StructSingular}}Known known{{StructSingular}})
        {
            return _Cache.Where(p => p.Key == known{{StructSingular}}).FirstOrDefault().Value;
        }

        static {{StructPlural}}Known()
        {
            _Cache = new Dictionary<{{StructSingular}}Known, {{StructSingular}}>
            {
{{CacheInitialization}}
            };

        }

        internal static void LoadKnown(List<{{StructSingular}}> list)
        {
            ArgumentNullException.ThrowIfNull(list);
            list.Clear();
            foreach (var p in _Cache)
                list.Add(p.Value);
        }
    }

    public sealed class {{StructPlural}}
    {
        public static List<{{StructSingular}}> AllKnown{{StructPlural}}
        {
            get
            {
                var returnValue = new List<{{StructSingular}}>();
                {{StructPlural}}Known.LoadKnown(returnValue);
                return returnValue;
            }
        }

        public static List<{{StructSingular}}> OrderByValue
        {
            get
            {
                var returnValue = new List<{{StructSingular}}>();
                {{StructPlural}}Known.LoadKnown(returnValue);
                return [.. returnValue.OrderBy(st => st.Value)];
            }
        }

        public static List<{{StructSingular}}> OrderByName
        {
            get
            {
                var returnValue = new List<{{StructSingular}}>();
                {{StructPlural}}Known.LoadKnown(returnValue);
                return [.. returnValue.OrderBy(st => st.Name)];
            }
        }

{{PluralStaticPublic}}
    }

    public class {{StructSingular}}JsonConverter: JsonConverter<{{StructSingular}}>
    {
        public override {{StructSingular}} Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string? val = reader.GetString();
                if (!string.IsNullOrWhiteSpace(val))
                {
                    {{StructSingular}} returnValue = ({{StructSingular}})val;
                    return returnValue;
                }
            }
            throw new JsonException("Expected a valid string value.");
        }

        public override void Write(Utf8JsonWriter writer, {{StructSingular}} value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}