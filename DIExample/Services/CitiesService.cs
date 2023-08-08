using ServiceContracts;

namespace Services
{
    public class CitiesService : ICitiesService
    {
        private Dictionary<int, string> _cities;

        public CitiesService()
        {
            _cities = new Dictionary<int, string>
            {
                { 1, "London" },
                { 2, "Mumbai" },
                { 3, "Los Angles" },
                { 4, "New York" }
            };
        }

        public Dictionary<int, string> GetCities()
        {
            return _cities;
        }
    }
}