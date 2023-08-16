using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppWeb.WebAPI.Controllers
{
    public class TestController : CustomControllerBase
    {
        [HttpGet]
        public string GetUser()
        {
            return "Hello World";
        }
    }
}
