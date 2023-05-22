using System.ComponentModel.DataAnnotations;

namespace WatchStock.Entities.Model.StockInfoModel
{
    public class CurrentValueResponseModel
    {
        [Key]
        public int StockId { get; set; }
        public string? StockSymbol { get; set; }
        public double CurrentPrice { get; set; }    
        public double PercentChange { get; set; }
        public double HighPriceOfTheDay { get; set; }
        public double LowPriceOfTheDay { get; set; }
        public string? Source { get; set; }  

    }
}
