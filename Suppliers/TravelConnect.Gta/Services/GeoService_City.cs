using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;
using TravelConnect.Gta.Interfaces;

namespace TravelConnect.Gta.Services
{
    public partial class GeoService : BaseService, IGeoService
    {
        private async Task<List<City>> RefreshCities(string countryCode)
        {
            var cities = await GetGtaCities(countryCode);
            _GeoRepository.InsertCities(cities, countryCode);

            return cities;
        }

        public async Task<List<City>> GetCities(string CountryCode, bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                return await RefreshCities(CountryCode);
            }

            var cities = await _GeoRepository.GetCities(CountryCode);

            if (cities.Count == 0)
                return await RefreshCities(CountryCode);
            else
                return cities;
        }

        public Task<City> GetCity(string Code, bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<City>> SearchCities(string cityName)
        {
            var cities = await _GeoRepository.SearchCities(cityName);

            return cities.Select(c => new City {
                Code = c.Code,
                CountryCode = c.CountryCode,
                Name = $"{c.Name}, {c.Country.Name}"
            }).ToList();
        }
    }
}