using WatchStock.Entities.Model.StockInfoModel;
namespace WatchStock.ServiceContracts.StockContract
{
    public interface IStockService
    {
        public Task<CurrentValueResponseModel> GetStockValues(string StockSymbol);
        public Task<CompanyProfile> GetCompanyDetails(string StockSymbol);

    }
}
