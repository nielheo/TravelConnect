using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.CommonServices;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre;
using TravelConnect.Sabre.Models;

namespace TravelConnect.Services
{
    public class GeoService : IGeoService
    {
        private ISabreConnector _SabreConnector;
        private IMemoryCache _cache;
        private ILogService _LogService;
        private readonly TCContext _context;

        public GeoService(ISabreConnector _SabreConnector,
            IMemoryCache _cache,
            ILogService _LogService,
            TCContext _context)
        {
            this._SabreConnector = _SabreConnector;
            this._cache = _cache;
            this._LogService = _LogService;
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

        private GeoCodeRQ ConvertToGeoCodeRQ(string airportCode)
        {
            GeoCodeRQ rq = new GeoCodeRQ
            {
                Request = new Class1[]
                {
                    new Class1
                    {
                        GeoCodeRQ = new Geocoderq {
                            PlaceById = new Placebyid
                            {
                                Id = airportCode,
                                BrowseCategory = new Browsecategory
                                {
                                        name = "AIR"
                                }
                            }
                        }
                    }
                }
            };

            return rq;
        }

        private AirportRS ConvertToAirportRS(GeoCodeRS response)
        {
            var place = response.Results?.FirstOrDefault()?.GeoCodeRS?.Place?.FirstOrDefault();

            if (place == null)
                return null;

            AirportRS rs = new AirportRS
            {
                Code = place.Id,
                Name = place.Name,
                Longitude = (decimal)place.longitude,
                Latitude = (decimal)place.latitude,
                CityName = place.City,
                CountryCode = place.Country
            };

            return rs;
        }

        private async Task<AirportRS> SubmitAirtportByCodeAsync(string airportCode)
        {
            string result = await _SabreConnector.SendRequestAsync("/v1/lists/utilities/geocode/locations",
                JsonConvert.SerializeObject(ConvertToGeoCodeRQ(airportCode).Request,
                    Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatString = "yyyy-MM-ddTHH:mm:ss"
                    }), true);

            GeoCodeRS rs = JsonConvert.DeserializeObject<GeoCodeRS>(result);

            return ConvertToAirportRS(rs);
        }

        public async Task<AirportRS> GetAirtportByCodeAsync(string airportCode)
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
                    cacheSearchRS = await SubmitAirtportByCodeAsync(airportCode);

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

        private async Task<bool> SaveAirportToDbAsync(AirportRS airportRs)
        {
            try
            {
                _context.Airports.Add(new Airport
                {
                    Id = airportRs.Code,
                    Name = airportRs.Name,
                    Longitude = (float)airportRs.Longitude,
                    Latitude = (float)airportRs.Latitude,
                    CountryCode = airportRs.CountryCode,
                    CityName = airportRs.CityName,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                });

                int x = await _context.SaveChangesAsync();

                return x > 0;
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex);
                return false;
            }
        }
    }
}