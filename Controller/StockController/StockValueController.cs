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
        private IStockService _currentValue;
        private ILogger<StockValueController> _logger;
        public StockValueController(IStockService currentValue, ILogger<StockValueController> logger )
        {
            _currentValue = currentValue;
            _logger = logger;
        }
          

        [HttpGet]
        [Route("{stockSymbol}")]
        public async Task<IActionResult> GetStockDetails(string stockSymbol)
        {
            _logger.LogInformation("GOING TO CONTROLLER");
            CurrentValueResponseModel? result = await _currentValue.GetStockValues(stockSymbol);
            _logger.LogInformation($"DATA FETCHED: {stockSymbol}");
            return Ok(result);
        }
    }
}
    