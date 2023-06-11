using Microsoft.AspNetCore.Mvc;
using WatchStock.Controller.StockController;
using WatchStock.Entities.Model.StockInfoModel;
using WatchStock.ServiceContracts.StockContract;

namespace WatchStock.Controller.CompanyController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private IStockService _stockService;
        private ILogger<StockValueController> _logger;
        public CompanyProfileController(IStockService stockService, ILogger<StockValueController> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{stockSymbol}")]
        public async Task<CompanyProfile> GetCompanyProfile(string stockSymbol)
        {
            _logger.LogInformation("GOING TO CONTROLLER");
            CompanyProfile result =  await _stockService.GetCompanyDetails(stockSymbol);
            _logger.LogInformation($"DATA FETCHED: {stockSymbol}");
            return result;
        }
    }
}
