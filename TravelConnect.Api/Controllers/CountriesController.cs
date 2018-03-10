using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Services;

namespace TravelConnect.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Countries")]
    public class CountriesController : Controller
    {
        private ICountryService _CountryService { get; set; }

        public CountriesController(ICountryService _CountryService)
        {
            this._CountryService = _CountryService;
        }

        [Route("{countryCode}/cities")]
        [HttpGet]
        public async Task<List<City>> GetCitiesInCountry(string countryCode)
        {
            return await _CountryService.GetCitiesInCountry(countryCode);
        }

        [Route("{countryCode}/cities/{code}")]
        [HttpGet]
        public async Task<City> GetCityByCode(string code, string countryCode)
        {
            return await _CountryService.GetCityByCode(code, countryCode);
        }

        [Route("permalink/{countryPermalink}/cities/{cityPermalink}")]
        [HttpGet]
        public async Task<City> GetCityByPermalink(string cityPermalink, string countryPermalink)
        {
            return await _CountryService.GetCityByPermalink(cityPermalink, countryPermalink);
        }

        [Route("")]
        [HttpGet]
        public async Task<List<Country>> GetCountries()
        {
            return await _CountryService.GetCountries();
        }

        [Route("permalink/{permalink}")]
        [HttpGet]
        public async Task<Country> GetCountryByPermalink(string permalink)
        {
            return await _CountryService.GetCountryByPermalink(permalink);
        }

        [Route("{code}")]
        [HttpGet]
        public async Task<Country> GetCountryByCode(string code)
        {
            return await _CountryService.GetCountryByCode(code);
        }

        [Route("/api/cityautocomplete")]
        [HttpGet]
        public async Task<List<CityAutocomplete>> GetCityAutocomplete(string query)
        {
            return await _CountryService.GetCityAutocomplete(query);
        }

        [Route("permalink/{permalink}/cities")]
        [HttpGet]
        public async Task<List<City>> GetCitiesInCountryByPermalink(string permalink)
        {
            return await _CountryService.GetCitiesInCountryByPermalink(permalink);
        }
    }
}