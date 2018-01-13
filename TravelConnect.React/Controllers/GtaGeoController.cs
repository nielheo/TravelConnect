using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;
using TravelConnect.Gta.Interfaces;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/GtaGeo")]
    public class GtaGeoController : Controller
    {
        private IGeoService _GeoService;

        public GtaGeoController(IGeoService _GeoService)
        {
            this._GeoService = _GeoService;
        }

        [Route("countries")]
        public async Task<List<Country>> Countries()
        {
            return await _GeoService.GetCountries();
        }

        [Route("countries/{code}")]
        public async Task<Country> Country(string Code)
        {
            return await _GeoService.GetCountry(Code);
        }

        [Route("countries/{countryCode}/cities")]
        public async Task<List<City>> Cities(string countryCode)
        {
            return await _GeoService.GetCities(countryCode);
        }

        [Route("searchcities")]
        public async Task<List<City>> SearchCities(string cityName)
        {
            var cities = await _GeoService.SearchCities(cityName);
            return cities
                .OrderBy(c => c.Name.ToLower().IndexOf(cityName.ToLower()))
                .ThenBy(c => c.Name).ToList();
        }
    }
}