using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace McGurkin.Tools.StructGenerator
{
    public class StructGenerator
    {
        public static string Generate(StructGeneratorSettings settings)
        {
            string returnValue = string.Empty;
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream? stream = assembly.GetManifestResourceStream("McGurkin.Tools.StructGenerator.StructTemplate.cs"))
                if (stream != null)
                {
                    using StreamReader reader = new(stream, Encoding.UTF8);
                    returnValue = reader.ReadToEnd();
                }

            if (!string.IsNullOrEmpty(returnValue))
            {
                returnValue = returnValue.Replace("{{Namespace}}", settings.Namespace);
                returnValue = returnValue.Replace("{{SingleKnownEnum}}", settings.SingleKnownEnum);
                returnValue = returnValue.Replace("{{StructPlural}}", settings.StructPlural);
                returnValue = returnValue.Replace("{{StructSingular}}", settings.StructSingular);
                returnValue = returnValue.Replace("{{CacheInitialization}}", settings.CacheInitialization);
                returnValue = returnValue.Replace("{{PluralStaticPublic}}", settings.PluralStaticPublic);
            }
            return returnValue;
        }
    }

    public class StructGeneratorSettings
    {
        public string Namespace { get; set; } = "McGurkin.ServiceProviders";

        public string StructSingular { get; set; } = "ResponseType";

        public string StructPlural { get; set; } = "ResponseTypes";

        public Dictionary<string, string> StructValues { get; set; } = new Dictionary<string, string>
        {
            { "Successfully completed", "Success" },
            { "Completed with warnings", "Warning" },
            { "Errors occurred", "Error" }
        };

        public string SingleKnownEnum
        {
            get
            {
                return string.Join(", ", StructValues.Values);
            }
        }

        public string CacheInitialization
        {
            get
            {
                string returnValue = string.Empty;
                var i = 1;
                foreach (var key in StructValues.Keys)
                {
                    string template = $"                {{ {StructSingular}Known.{StructValues[key]}, new {StructSingular}({i}, \"{key}\", \"{StructValues[key]}\") }}";
                    returnValue += template + ", \r\n";
                    i++;
                }

                if (returnValue.Length > 4) returnValue = returnValue.Substring(0, returnValue.Length - 4);
                return returnValue;
            }
        }

        public string PluralStaticPublic
        {
            get
            {
                string returnValue = string.Empty;
                foreach (var key in StructValues.Keys)
                {
                    string template = $"        public static {StructSingular} {StructValues[key]} {{ get {{ return {StructPlural}Known.FromKnown({StructSingular}Known.{StructValues[key]}); }} }}\r\n";
                    returnValue += template;
                }

                if (returnValue.Length > 2) returnValue = returnValue.Substring(0, returnValue.Length - 2);
                return returnValue;
            }
        }
    }
}
