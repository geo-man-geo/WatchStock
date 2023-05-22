using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchStock.Entities.Model.StockInfoModel;
using WatchStock.ServiceContracts.StockContract;

namespace WatchStock.Controller.StockController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockValueController : ControllerBase
    {
        private ICurrentValue _currentValue;
       public StockValueController(ICurrentValue currentValue)
       {
            _currentValue = currentValue;
       }

        // GET: api/<MovieController>
        [HttpGet]
        [Route("{stockSymbol}")]
        public async Task<CurrentValueResponseModel> AddStock(string stockSymbol)
        {
            CurrentValueResponseModel? result = await _currentValue.AddStock(stockSymbol);
            return result;
        }

        [HttpGet]
        [Route("db/{stockSymbol}")]
        public async Task<CurrentValueResponseModel> GetStock(string stockSymbol)
        {
            CurrentValueResponseModel? result = await _currentValue.GetStock(stockSymbol);
            return result;
        }
    }
}
