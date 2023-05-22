using WatchStock.Entities.Model.StockInfoModel;

namespace WatchStock.RepositoryContracts.StockRepositoryContract
{
    public interface ICurrentValueRepository
    {
        Task<CurrentValueResponseModel> GetCurrentStockValue(string StockSymbol);
        Task AddCurrentStockValue(CurrentValueResponseModel currentValueResponseModel);
    }
}
