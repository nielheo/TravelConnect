using System.Collections.Generic;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;

namespace TravelConnect.Gta.DataServices
{
    public interface IGeoRepository
    {
        Task<List<Country>> GetCountries();

        Task<Country> GetCountry(string Code);

        void InsertCountries(List<Country> Countries);

        Task<List<City>> GetCities(string CountryCode);

        Task<City> GetCity(string Code);

        void InsertCities(List<City> Cities, string CountryCode);
    }
}