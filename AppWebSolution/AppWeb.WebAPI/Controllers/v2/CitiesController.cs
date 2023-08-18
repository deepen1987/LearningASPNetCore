using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppWeb.Infrastructure.DatabaseContext;

namespace AppWeb.WebAPI.Controllers.v2
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("2.0")]
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// To get list of cities (including city id and city name) from cities table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string?>>> GetCities()
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities.ToListAsync();

            List<string?> citiesList = cities.Select(city => city.CityName).ToList();
            return citiesList;
        }
    }
}
