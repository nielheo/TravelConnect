using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models;

namespace TravelConnect.Services
{
    public class CountryService : ICountryService
    {
        private TCContext _db { get; set; }
        //private readonly ILogger _logger;

        //protected LogService _LogService = null;

        public CountryService(TCContext db)
        {
            _db = db;
            //    _logger = logger;
        }

        public async Task<List<City>> GetCitiesInCountry(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))
                return new List<City>();

            return await _db.Cities.Where(c => c.CountryCode.ToLower() == countryCode.ToLower()).ToListAsync();
        }

        public async Task<City> GetCityByCode(string code, string countryCode)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(countryCode))
                return null;

            return await _db.Cities.Where(c =>
                c.Code.ToLower() == code.ToLower()
                && c.Country.Code.ToLower() == countryCode.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<City> GetCityByPermalink(string cityPermalink, string countryPermalink)
        {
            if (string.IsNullOrEmpty(countryPermalink) || string.IsNullOrEmpty(cityPermalink))
                return null;

            return await _db.Cities.Where(c =>
                c.Permalink.ToLower() == cityPermalink.ToLower()
                && c.Country.Permalink.ToLower() == countryPermalink.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _db.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryByPermalink(string permalink)
        {
            if (string.IsNullOrEmpty(permalink))
                return null;

            return await _db.Countries.Where(c =>
                c.Permalink.ToLower() == permalink.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<Country> GetCountryByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            return await _db.Countries.Where(c =>
                c.Code.ToLower() == code.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<List<CityAutocomplete>> GetCityAutocomplete(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<CityAutocomplete>();

            name = name.ToLower();
            return await _db.Cities.Include(c => c.Country)
                .Where(c => c.Name.ToLower().IndexOf(name) > -1)
                .Select(c => new CityAutocomplete
                {
                    City = c.Permalink,
                    Country = c.Country.Permalink,
                    Display = c.NameLong,
                })
                .OrderBy(c => c.Display.ToLower().IndexOf(name))
                .ThenBy(c => c.Display)
                .Take(30)
                .ToListAsync();
        }

        public async Task<List<City>> GetCitiesInCountryByPermalink(string permalink)
        {
            if (string.IsNullOrEmpty(permalink))
                return new List<City>();

            return await _db.Cities
                .Where(c => c.Country.Permalink.ToLower() == permalink.ToLower())
                .ToListAsync();
        }

        
    }
}