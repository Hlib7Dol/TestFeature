using System.Text.Json;

namespace FeatureConsoleApplication.Services
{
    internal class RequestService : IRequestService
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7138/api/")
        };

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();

            var deserializeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var deserializedObject = JsonSerializer.Deserialize<T>(responseString, deserializeOptions);
            if(deserializedObject == null)
            {
                throw new ArgumentNullException(nameof(deserializedObject));
            }

            return deserializedObject;
        }
    }
}
