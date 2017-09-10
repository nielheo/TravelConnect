using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre;
using TravelConnect.Sabre.Models;

namespace TravelConnect.Services
{
    public class GeoService : IGeoService
    {
        private ISabreConnector _SabreConnector;
        private IMemoryCache _cache;

        public GeoService(ISabreConnector _SabreConnector,
            IMemoryCache _cache)
        {
            this._SabreConnector = _SabreConnector;
            this._cache = _cache;
        }

        public async Task<AirportAutocompleteRS> GetAirportAutocompleteAsync(string query)
        {
            string key = string.Format("AirportAutocomplete_{0}", query);
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
            string key = string.Format("Airport_{0}", airportCode);
            AirportRS cacheSearchRS;

            if (!_cache.TryGetValue(key, out cacheSearchRS))
            {
                // Key not in cache, so get data.
                cacheSearchRS = await SubmitAirtportByCodeAsync(airportCode);

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                // Save data in cache.
                _cache.Set(key, cacheSearchRS, cacheEntryOptions);
            }

            return cacheSearchRS;
        }
    }
}