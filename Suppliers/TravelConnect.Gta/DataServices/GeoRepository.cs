using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.CommonServices;
using TravelConnect.Gta.DataModels;

namespace TravelConnect.Gta.DataServices
{
    public class GeoRepository : IGeoRepository
    {
        private GtaContext _db { get; set; }
        //private readonly ILogger _logger;

        protected LogService _LogService = null;

        public GeoRepository(GtaContext db, ILogger<GeoRepository> logger)
        {
            _db = db;
            //_logger = logger;
        }

        public async Task<List<City>> GetCities(string CountryCode)
        {
            return await _db.Cities.Where(c => c.CountryCode == CountryCode).ToListAsync();
        }

        public async Task<City> GetCity(string Code)
        {
            return await _db.Cities.FirstOrDefaultAsync(c => c.Code == Code);
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _db.Countries.ToListAsync();
        }

        public async Task<Country> GetCountry(string Code)
        {
            return await _db.Countries.FirstOrDefaultAsync(c => c.Code == Code);
        }

        public async void InsertCountries(List<Country> Countries)
        {
            var dbCountries = await _db.Countries.ToListAsync();

            foreach(var country in Countries)
            {
                if (!dbCountries.Any(c => c.Code == country.Code))
                    _db.Countries.Add(new Country
                    {
                        Code = country.Code,
                        Name = country.Name
                    });
            }

            await _db.SaveChangesAsync();
        }
        
        public  async void InsertCities(List<City> Cities, string CountryCode)
        {
            var dbCities = await _db.Cities.Where(c => c.CountryCode == CountryCode).ToListAsync();

            foreach(var city in Cities.Where(c=>c.CountryCode == CountryCode))
            {
                if (!dbCities.Any(c => c.Code == city.Code))
                    _db.Cities.Add(new City
                    {
                        Code = city.Code,
                        Name = city.Name,
                        CountryCode = city.CountryCode
                    });
            }

            await _db.SaveChangesAsync();
        }
    }
}