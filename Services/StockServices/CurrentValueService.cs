using System.Text.Json;
using WatchStock.Entities.Model.StockInfoModel;
using WatchStock.Repositories.StockRepository;
using WatchStock.RepositoryContracts.StockRepositoryContract;
using WatchStock.ServiceContracts.StockContract;
namespace WatchStock.Services.StockServices
{
    public class CurrentValueService : ICurrentValue
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICurrentValueRepository _currentValueRepository;
        public CurrentValueService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ICurrentValueRepository currentValueRepository) 
        { 
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _currentValueRepository = currentValueRepository;
        }
        public async Task<CurrentValueResponseModel> AddStock(string stockSymbol)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}") //URI includes the secret token
            };
            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync( httpRequestMessage );

            string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            if (responseDictionary == null)
                throw new InvalidOperationException("No response from server");

            if (responseDictionary.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
            
            CurrentValueResponseModel currentValueResponseModel = new CurrentValueResponseModel();
            currentValueResponseModel.StockSymbol = stockSymbol;
            currentValueResponseModel.CurrentPrice = Double.Parse(responseDictionary["c"].ToString());
            currentValueResponseModel.PercentChange = Double.Parse(responseDictionary["pc"].ToString());
            currentValueResponseModel.LowPriceOfTheDay = Double.Parse(responseDictionary["l"].ToString());
            currentValueResponseModel.HighPriceOfTheDay = Double.Parse(responseDictionary["h"].ToString());
            currentValueResponseModel.Source = "Finnhub.io, going to DB";

            _currentValueRepository?.AddCurrentStockValue( currentValueResponseModel );

            return currentValueResponseModel;
        }

        public async Task<CurrentValueResponseModel> GetStock(string StockSymbol)
        {
            CurrentValueResponseModel currentValueResponseModel = new CurrentValueResponseModel();
            currentValueResponseModel = await _currentValueRepository.GetCurrentStockValue( StockSymbol );
            return currentValueResponseModel;
        }
    }
}
