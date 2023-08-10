using Microsoft.AspNetCore.Mvc;
using StockApp.Models;
using StockApp.ServiceContracts;

namespace StockApp.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;

        public TradeController(IFinnhubService finnhubService)
        {
            _finnhubService = finnhubService;
        }

        [Route("/Trade/Index")]
        public async Task<IActionResult> Index(string symbol = "MSFT")
        {
            if(symbol == null)
            {
                return NotFound();
            }

            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(symbol);
            Dictionary<string, object>? companyStockPrice = await _finnhubService.GetStockPriceQuote(symbol);

            StockTrade stockTrade = new StockTrade()
            {
                StockName = Convert.ToString(companyProfile["name"].ToString()),
                StockSymbol = symbol,
                Price = Convert.ToDouble(companyStockPrice["c"].ToString()),
                Quantity = Convert.ToDouble(companyProfile["shareOutstanding"].ToString())
            };


            return Ok(stockTrade);
        }
    }
}
