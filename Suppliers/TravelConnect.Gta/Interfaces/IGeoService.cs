using System.Collections.Generic;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;

namespace TravelConnect.Gta.Interfaces
{
    public interface IGeoService
    {
        Task<List<Country>> GetCountries(bool forceRefresh = false);

        Task<Country> GetCountry(string Code, bool forceRefresh = false);

        Task<List<City>> GetCities(string CountryCode, bool forceRefresh = false);

        Task<City> GetCity(string Code, bool forceRefresh = false);

        Task<List<City>> SearchCities(string cityName);
    }
}