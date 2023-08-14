using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class CountryServiceTests
    {
        private readonly ICountriesService _countriesService;

        public CountryServiceTests()
        {
            _countriesService = new CountriesService();
        }

        //When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() => 
            {
                _countriesService.AddCountry(request);
            });
        }

        //When the CountryName is null, it should throw ArgumentException

        //When the CountryName is duplicate, it should throw ArgumentException

        //When the Country Name is good, it should insert(add) the country to the existing list of countries.
    }
}