using ControllersExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
            return Content("Home here", "text/plain");
        }

        [Route("about")]
        public string About()
        {
            return "About here";
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person p1 = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Test1",
                Age = 35
            };

            return Json(p1);
        }

        [Route("file-download")]
        public VirtualFileResult GetPdfFile()
        {
            return new VirtualFileResult("/sample.pdf", "application/pdf");
        }

        [Route("booksstore")]
        public IActionResult GetBooks()
        {
            // This is the previous code
            //return Ok();

            //This is the redirected Code
            //return new RedirectToActionResult("GetBooks", "Store", new { }, true);
            return RedirectToActionPermanent("GetBooks", "Store");
        }
    }
}
