using System.Collections.Generic;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;

namespace TravelConnect.Gta.DataServices
{
    public interface IGeoRepository
    {
        Task<List<Country>> GetCountries();

        Task<Country> GetCountry(string Code);

        Task InsertCountries(List<Country> Countries);

        Task<List<City>> GetCities(string CountryCode);

        Task<City> GetCity(string Code);

        Task<List<City>> SearchCities(string cityName);

        Task InsertCities(List<City> Cities, string CountryCode);
    }
}