using StockApp.ServiceContracts;
using System.Text.Json;

namespace StockApp.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string? _apiKey;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _apiKey = _configuration.GetValue<string>("APIKey");
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_apiKey}"),
                Method = HttpMethod.Get,
            };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            Stream stream = httpResponseMessage.Content.ReadAsStream();
            StreamReader streamReader = new (stream);

            string response = streamReader.ReadToEnd();
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

            if(responseDictionary == null)
                throw new InvalidOperationException("No response from Finnhub Service.");
            
            if(responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException($"{responseDictionary["error"]}");

            return responseDictionary;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpRequestMessage httpRequestMessage = new()
            {
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_apiKey}"),
                Method = HttpMethod.Get
            };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            Stream stream = httpResponseMessage.Content.ReadAsStream();
            StreamReader streamReader = new(stream);

            string response = streamReader.ReadToEnd();

            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from Finnhub Service.");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException($"{responseDictionary["error"]}");

            return responseDictionary;
        }
    }
}
