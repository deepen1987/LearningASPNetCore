using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppWeb.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TestController : CustomControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetUser()
        {
            return "Hello World";
        }
    }
}
