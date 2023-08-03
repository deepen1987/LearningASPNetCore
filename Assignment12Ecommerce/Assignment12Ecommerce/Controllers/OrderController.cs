using Assignment12Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Assignment12Ecommerce.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Ok("Home Page");
        }

        [HttpPost("/order")]
        public IActionResult CreateOrder(Order order)
        {
            if(!ModelState.IsValid)
            {
                string errorMessages = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));

                return BadRequest(errorMessages);
            }

            return Json(new { orderNumber = (order.OrderNo) });
        }
    }
}
