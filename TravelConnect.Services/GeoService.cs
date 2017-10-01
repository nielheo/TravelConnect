using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelConnect.CommonServices;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre;
using TravelConnect.Sabre.Interfaces;
using TravelConnect.Sabre.Models;

namespace TravelConnect.Services
{
    public class GeoService : IGeoService
    {
        private ISabreConnector _SabreConnector;
        private IMemoryCache _cache;
        private ILogService _LogService;
        private IUtilityService _UtilityService;
        private readonly TCContext _context;

        public GeoService(ISabreConnector _SabreConnector,
            IMemoryCache _cache,
            ILogService _LogService,
            IUtilityService _UtilityService,
            TCContext _context)
        {
            this._SabreConnector = _SabreConnector;
            this._cache = _cache;
            this._LogService = _LogService;
            this._UtilityService = _UtilityService;
            this._context = _context;
        }

        public async Task<AirportAutocompleteRS> GetAirportAutocompleteAsync(string query)
        {
            query = query.ToLower();
            string key = string.Format("airportAutocomplete_{0}", query);
            AirportAutocompleteRS cache;

            if (!_cache.TryGetValue(key, out cache))
            {
                // Key not in cache, so get data.
                cache = await SubmitGetAirportAutocompleteAsync(query);

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                // Save data in cache.
                _cache.Set(key, cache, cacheEntryOptions);
            }

            return cache;
        }

        private async Task<AirportAutocompleteRS> SubmitGetAirportAutocompleteAsync(string query)
        {
            var result = await _SabreConnector.SendRequestAsync("/v1/lists/utilities/geoservices/autocomplete",
                "query=" + query + "&category=AIR&limit=30", false);

            result = result.Replace("category:AIR", "categoryAIR");

            AutocompleteRS rs = JsonConvert.DeserializeObject<AutocompleteRS>(result);

            AirportAutocompleteRS autocompleteRs = new AirportAutocompleteRS()
            {
                AirportsRS = new System.Collections.Generic.List<AirportRS>()
            };

            foreach (var doc in rs.Response.grouped.categoryAIR.doclist.docs)
            {
                autocompleteRs.AirportsRS.Add(new AirportRS
                {
                    Code = doc.id,
                    Name = doc.name,
                    IataCityCode = doc.iataCityCode,
                    CityName = doc.city,
                    CountryCode = doc.country,
                    CountryName = doc.countryName,
                });
            }

            return autocompleteRs;
        }

        public async Task<AirportRS> GetAirtportByCodeAsync(string airportCode)
        {
            try
            {
                airportCode = airportCode.ToUpper();
                string key = string.Format("airport_{0}", airportCode);
                AirportRS cacheSearchRS;

                if (!_cache.TryGetValue(key, out cacheSearchRS))
                {
                    // Key not in cache, so get from database.
                    cacheSearchRS = await RetrieveAirportFromDbAsync(airportCode);

                    //Key not in database, so get from Sabre.
                    if (cacheSearchRS == null)
                    {
                        cacheSearchRS = await _UtilityService.AirportLookup(airportCode);

                        //If get value, save into database
                        if (cacheSearchRS != null)
                        {
                            await SaveAirportToDbAsync(cacheSearchRS);
                        }
                    }

                    //Key has value, store in cache
                    if (cacheSearchRS != null)
                    {
                        // Set cache options.
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            // Keep in cache for this time, reset time if accessed.
                            .SetSlidingExpiration(TimeSpan.FromHours(1));

                        // Save data in cache.
                        _cache.Set(key, cacheSearchRS, cacheEntryOptions);
                    }
                }

                return cacheSearchRS;
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                var fullMethodName = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;

                _LogService.LogException(ex, fullMethodName);

                return null;
            }
        }

        private async Task<AirportRS> RetrieveAirportFromDbAsync(string code)
        {
            var airport = await _context.Airports
                .SingleOrDefaultAsync(a => a.Id == code);

            if (airport == null)
                return null;

            return new AirportRS
            {
                Code = airport.Id,
                Name = airport.Name,
                CityName = airport.CityName,
                CountryCode = airport.CountryCode,
                Longitude = (decimal)airport.Longitude,
                Latitude = (decimal)airport.Latitude
            };
        }

        private async Task<Country> RetrieveCountryFromDbAsync(string code)
        {
            var country = await _context.Countries.SingleOrDefaultAsync(c => c.Id == code);
            if (country == null)
            {
                country = _context.Countries.Add(new Country
                {
                    Id = code,
                    Name = code,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now
                }).Entity;
            }

            return country;
        }

        private async Task<bool> SaveAirportToDbAsync(AirportRS airportRs)
        {
            try
            {
                Country country = await RetrieveCountryFromDbAsync(airportRs.CountryCode);
                country.Airports = new List<Airport>();
                country.Airports.Add(new Airport
                {
                    Id = airportRs.Code,
                    Name = airportRs.Name,
                    Longitude = (float)airportRs.Longitude,
                    Latitude = (float)airportRs.Latitude,
                    //CountryCode = airportRs.CountryCode,
                    CityName = airportRs.CityName,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                });

                int x = await _context.SaveChangesAsync();

                return x > 0;
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                var fullMethodName = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;

                _LogService.LogException(ex, fullMethodName);

                return false;
            }
        }
    }
}