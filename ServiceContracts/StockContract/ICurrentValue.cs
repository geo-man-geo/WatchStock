using WatchStock.Entities.Model.StockInfoModel;
namespace WatchStock.ServiceContracts.StockContract
{
    public interface ICurrentValue
    {
        public Task<CurrentValueResponseModel> AddStock(string StockSymbol);

        public Task<CurrentValueResponseModel> GetStock(string StockSymbol);
    }
}
