using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Infra.Repository.Api
{
    public class ApiService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiService(string baseAddress)
        {
            _baseAddress = baseAddress;
            _httpClient = new HttpClient { BaseAddress = new Uri(_baseAddress) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<T> GetAsync(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, _serializerOptions)!;
        }

        public async Task<T> PostAsync(string uri, T data)
        {
            var content = new StringContent(JsonSerializer.Serialize(data, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseData, _serializerOptions)!;
        }

        public async Task<T> PutAsync(string uri, T data)
        {
            var content = new StringContent(JsonSerializer.Serialize(data, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseData, _serializerOptions)!;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
