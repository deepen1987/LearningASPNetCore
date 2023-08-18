using Microsoft.AspNetCore.Mvc;

namespace AppWeb.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
    }
}