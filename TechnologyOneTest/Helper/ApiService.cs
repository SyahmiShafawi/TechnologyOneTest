using Microsoft.Extensions.Options;
using System.Text.Json;

namespace TechnologyOneTest.Helper
{
    public class ApiService
    {
        private readonly string _baseUrl;

        public ApiService(IOptions<AppSettings> appSettings)
        {
            _baseUrl = appSettings.Value.BaseUrl;
        }

        public async Task<string> ConvertNumberToWordsAsync(string number)
        {
            using var httpClient = new HttpClient();
            var formData = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("number", number)
        });

            var response = await httpClient.PostAsync($"{_baseUrl}/api/v1/apihandler/ConvertNumberToWords", formData);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ConversionResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result?.Words ?? "Conversion failed.";
            }

            throw new Exception($"API call failed with status code: {response.StatusCode}");
        }
    }

    public class ConversionResponse
    {
        public string Words { get; set; }
    }
}
