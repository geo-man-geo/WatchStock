namespace WatchStock.Entities.Model.StockInfoModel
{
    public class CompanyProfile
    {
        public string Country { get; set; }     
        public string Currency { get; set; }
        public string Exchange { get; set; }    
        public string finnhubIndustry { get; set; }
        public DateOnly IPO { get; set; }
        public string StockName { get; set;}
        
    }
}
