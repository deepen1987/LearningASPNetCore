using Microsoft.AspNetCore.Mvc;

namespace AppWeb.WebAPI.Controllers
{
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
    }
}