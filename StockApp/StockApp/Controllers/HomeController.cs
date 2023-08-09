using Microsoft.AspNetCore.Mvc;
using StockApp.Models;
using StockApp.Services;

namespace StockApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnhubService _finnhubService;

        public HomeController(FinnhubService finnhubService)
        {
            _finnhubService = finnhubService;
        }

        [Route("/")]
        public async Task<IActionResult> Index(string symbol = "MSFT")
        {
            if(symbol == null)
                return NotFound();

            Dictionary<string, object>? responseDictionary = await _finnhubService.GetStockPriceQuote(symbol);
            Stock stock = new Stock()
            {
                StockSymbol = symbol,
                CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
                Change = Convert.ToDouble(responseDictionary["d"].ToString()),
                PercentageChange = Convert.ToString(responseDictionary["dp"].ToString()),
                HighPriceOfTheDay = Convert.ToDouble(responseDictionary["h"].ToString()),
                LowPrceOfTheDay = Convert.ToDouble(responseDictionary["l"].ToString()),
                OpenPriceOfTheDay = Convert.ToDouble(responseDictionary["o"].ToString()),
                PreviousClosePriceOfTheDay = Convert.ToDouble(responseDictionary["pc"].ToString())
            };

            return Ok(stock);
        }
    }
}
