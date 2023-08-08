using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace DIExample.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICitiesService _citiesServices;

        public CitiesController(ICitiesService citiesServices)
        {
            //Here we would have to create a object of the cities service class but this
            //is a problem hence will see how to do Dependency Injection
            // _citiesServices = new CitiesService();
            _citiesServices = citiesServices;
        }

        [Route("cities/")]
        public IActionResult GetCities()
        {
            Dictionary<int, string> result = _citiesServices.GetCities();

            if(result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
