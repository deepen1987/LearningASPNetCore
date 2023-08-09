namespace StockApp.Models
{
    public class Stock
    {
        public string? StockSymbol { get; set; }
        public double CurrentPrice { get; set; }
        public double Change { get; set; }
        public string? PercentageChange { get; set; }
        public double HighPriceOfTheDay { get; set; }
        public double LowPrceOfTheDay { get; set; }
        public double OpenPriceOfTheDay { get; set; }
        public double PreviousClosePriceOfTheDay { get; set; }

    }
}
