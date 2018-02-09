using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Models;

namespace TravelConnect.Interfaces
{
    public interface ICountryService
    {
        Task<Country> GetCountryByCode(string code);

        Task<Country> GetCountryByPermalink(string permalink);

        Task<List<Country>> GetCountries();

        Task<List<City>> GetCitiesInCountry(string countryCode);

        Task<List<City>> GetCitiesInCountryByPermalink(string permalink);

        Task<City> GetCityByCode(string code, string countryCode);

        Task<City> GetCityByPermalink(string countryPermalink, string cityPermalink);

        Task<List<CityAutocomplete>> GetCityAutocomplete(string name);
    }
}
