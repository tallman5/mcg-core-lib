using McGurkin.Runtime.Serialization;
using System.Net.Http.Headers;
using System.Text;

namespace McGurkin.Net.Http
{
    public partial class ServiceCaller
    {
        private static void ApplySettings(HttpClient httpClient, string? bearerToken, Dictionary<string, string>? headers)
        {
            if (!string.IsNullOrWhiteSpace(bearerToken))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            if (null != headers)
            {
                httpClient.DefaultRequestHeaders.Clear();
                foreach (var header in headers)
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        public static async Task<HttpResponseMessage> GetAsync(HttpClient httpClient, string url, string? bearerToken, Dictionary<string, string>? headers)
        {
            ApplySettings(httpClient, bearerToken, headers);
            var returnValue = await httpClient.GetAsync(url);
            return ProcessResponse(returnValue);
        }

        public static async Task<HttpResponseMessage> GetAsync(string url, string? bearerToken, Dictionary<string, string>? headers)
        {
            using var httpClient = new HttpClient();
            var returnValue = await GetAsync(httpClient, url, bearerToken, headers);
            return returnValue;
        }

        public static async Task<T> GetAsync<T>(HttpClient httpClient, string url, string? bearerToken, Dictionary<string, string>? headers)
        {
            var rs = await GetAsync(httpClient, url, bearerToken, headers);
            var contentString = await rs.Content.ReadAsStringAsync();
            T returnValue = Serializer.FromString<T>(contentString);
            return returnValue;
        }

        public static async Task<T> GetAsync<T>(string url, string? bearerToken, Dictionary<string, string>? headers)
        {
            var rs = await GetAsync(url, bearerToken, headers);
            var contentString = await rs.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(string))
                return (T)(object)contentString;

            T returnValue = Serializer.FromString<T>(contentString);
            return returnValue;
        }

        public static async Task<HttpResponseMessage> PostAsync(HttpClient httpClient, string url, object o, string? bearerToken, Dictionary<string, string>? headers)
        {
            ApplySettings(httpClient, bearerToken, headers);
            var objectString = (o is string) ? o as string : Serializer.ToString(o);
            var content = new StringContent(objectString, Encoding.UTF8, "application/json");
            var returnValue = await httpClient.PostAsync(url, content);
            return ProcessResponse(returnValue);
        }

        public static async Task<HttpResponseMessage> PostAsync(string url, object o, string? bearerToken, Dictionary<string, string>? headers)
        {
            using var httpClient = new HttpClient();
            var returnValue = await PostAsync(httpClient, url, o, bearerToken, headers);
            return returnValue;
        }

        public static async Task<T> PostAsync<T>(HttpClient httpClient, string url, object o, string? bearerToken, Dictionary<string, string>? headers)
        {
            var rs = await PostAsync(httpClient, url, o, bearerToken, headers);
            var contentString = await rs.Content.ReadAsStringAsync();
            T returnValue = Serializer.FromString<T>(contentString);
            return returnValue;
        }

        public static async Task<T> PostAsync<T>(string url, object o, string? bearerToken, Dictionary<string, string>? headers)
        {
            var rs = await PostAsync(url, o, bearerToken, headers);
            var contentString = await rs.Content.ReadAsStringAsync();
            T returnValue = Serializer.FromString<T>(contentString);
            return returnValue;
        }

        private static HttpResponseMessage ProcessResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return response;

            var msg = $"An error occurred calling: {response.ReasonPhrase}";
            var contentString = response.Content.ReadAsStringAsync().Result;
            throw new Exception(msg, new Exception(contentString));
        }
    }
}
