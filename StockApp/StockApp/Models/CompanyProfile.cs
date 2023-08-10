namespace StockApp.Models
{
    public class CompanyProfile
    {
        public string? Country { get; set; }
        public string? Currency { get; set; }
        public string? Exchange { get; set; }
        public DateOnly Ipo { get; set; }
        public int MarketCapatilization { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public double ShareOutstanding { get; set; }
        public string? Ticker { get; set; }
        public string? WebUrl { get; set; }
        public string? Logo { get; set; }
        public string? FinHubIndustry { get; set;}
    }
}
