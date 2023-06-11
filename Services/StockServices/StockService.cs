using System.Text.Json;
using WatchStock.Entities.Model.StockInfoModel;
using WatchStock.ServiceContracts.StockContract;
using WatchStock.Utitlies.ThirdPartyClient;

namespace WatchStock.Services.StockServices
{
    public class StockService : IStockService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public StockService(IConfiguration configuration, IHttpClientFactory httpClientFactory) 
        { 
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CompanyProfile> GetCompanyDetails(string StockSymbol)
        {
            Uri requestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={StockSymbol}&token=cgtqn4hr01qoiqvp6of0cgtqn4hr01qoiqvp6ofg");   
            Dictionary<string,object>? responseDictionary = await HttpClientHelper.GetResponse(requestUri);

            CompanyProfile companyProfile = new CompanyProfile();
            companyProfile.Country = responseDictionary["country"].ToString();
            companyProfile.Currency = responseDictionary["currency"].ToString();
            companyProfile.Exchange = responseDictionary["exchange"].ToString();
            companyProfile.IPO = responseDictionary["ipo"].ToString();
            companyProfile.MarketCapitalization = double.Parse(responseDictionary["marketCapitalization"].ToString());
            companyProfile.StockName = responseDictionary["name"].ToString();
            companyProfile.Phone = responseDictionary["phone"].ToString();
            companyProfile.ShareOutstandingeNumber = double.Parse(responseDictionary["shareOutstanding"].ToString());
            companyProfile.Ticker = responseDictionary["ticker"].ToString();
            companyProfile.Weburl = responseDictionary["weburl"].ToString();
            companyProfile.Logo = responseDictionary["logo"].ToString();
            companyProfile.finnhubIndustry = responseDictionary["finnhubIndustry"].ToString();

            return companyProfile;

        }

        public async Task<CurrentValueResponseModel> GetStockValues(string stockSymbol)
        {

            Uri requestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token=cgtqn4hr01qoiqvp6of0cgtqn4hr01qoiqvp6ofg");
            Dictionary<string, object>? responseDictionary = await HttpClientHelper.GetResponse(requestUri);
            
            CurrentValueResponseModel currentValueResponseModel = new CurrentValueResponseModel();
            currentValueResponseModel.StockSymbol = stockSymbol;
            currentValueResponseModel.CurrentPrice = Double.Parse(responseDictionary["c"].ToString());
            currentValueResponseModel.PercentChange = Double.Parse(responseDictionary["pc"].ToString());
            currentValueResponseModel.LowPriceOfTheDay = Double.Parse(responseDictionary["l"].ToString());
            currentValueResponseModel.HighPriceOfTheDay = Double.Parse(responseDictionary["h"].ToString());
            return currentValueResponseModel;
        }

    }
}
