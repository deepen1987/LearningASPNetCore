using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    [Controller]
    public class StoreController : Controller
    {
        [Route("store/books")]
        public IActionResult GetBooks()
        {
            return Ok("Books");
        }
    }
}
