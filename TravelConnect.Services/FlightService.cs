using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.CommonServices;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre.Interfaces;

namespace TravelConnect.Services
{
    public class FlightService : IFlightService
    {
        private IMemoryCache _cache;
        private IAirService _AirService;
        private IUtilityService _UtilityService;
        private ILogService _LogService;

        private readonly TCContext _context;

        public FlightService(IAirService _AirService,
            IUtilityService _UtilityService,
            ILogService _LogService,
            IMemoryCache memoryCache,
            TCContext _context)
        {
            this._cache = memoryCache;
            this._AirService = _AirService;
            this._UtilityService = _UtilityService;
            this._LogService = _LogService;
            this._context = _context;
        }

        private List<string> GetAirlines(FlightSearchRS rs)
        {
            if (rs == null)
                return new List<string>();
            return rs.PricedItins.SelectMany(p => p.Legs.SelectMany(l => l.Segments.Select(s => s.MarketingFlight.Airline)))
                .Distinct().ToList();
        }

        private async Task<FlightSearchRS> RetrieveAirLowFareSearch(FlightSearchRQ request)
        {
            string sRequest = "AirLowFare_" + JsonConvert.SerializeObject(request);
            FlightSearchRS cacheSearchRS;

            if (!_cache.TryGetValue(sRequest, out cacheSearchRS))
            {
                // Key not in cache, so get data from Sabre
                cacheSearchRS = await _AirService.AirLowFareSearchAsync(request);

                if (cacheSearchRS == null)
                {
                    cacheSearchRS = new FlightSearchRS
                    {
                        PricedItins = new List<PricedItin>()
                    };
                }
                else
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                    // Save data in cache.
                    _cache.Set(sRequest, cacheSearchRS, cacheEntryOptions);
                }
            }

            return cacheSearchRS;
        }

        public async Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request)
        {
            Task<FlightSearchRS> rs = RetrieveAirLowFareSearch(request);
            Task<FlightSearchRS> ow = null;

            if (request.Segments.Count > 1 || request.Airlines?.Count == 0)
            {
                request.Segments = new List<SegmentRQ>
                {
                    request.Segments.First()
                };
                ow = RetrieveAirLowFareSearch(request);
            }

            FlightSearchRS response = await rs;

            if (request.Segments.Count == 1 || (request.Airlines?.Count ?? 0) > 0)
            {
                response.Airlines = GetAirlines(response);
            }
            else
            {
                FlightSearchRS owResponse = await ow;
                response.Airlines = ((owResponse?.PricedItins?.Count ?? 0) > 0)
                    ? GetAirlines(owResponse)
                    : GetAirlines(response);
            }

            return response;
        }

        private async Task<Models.Responses.AirlineRS> RetrieveAirlineFromDbAsync(string code)
        {
            var airline = await _context.Airlines
                .SingleOrDefaultAsync(a => a.Id == code);

            if (airline == null)
                return null;

            return new Models.Responses.AirlineRS
            {
                Code = airline.Id,
                Name = airline.Name
            };
        }

        private async Task<bool> SaveAirlineToDbAsync(string id, string name)
        {
            try
            {
                _context.Airlines.Add(new Airline
                {
                    Id = id,
                    Name = name,
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

        public async Task<Models.Responses.AirlineRS> AirlineByCodeAsync(string code)
        {
            code = code.ToLower();
            string key = "airline_" + code;
            Models.Responses.AirlineRS cache;

            if (!_cache.TryGetValue(key, out cache))
            {
                // Key not in cache, so get from database.
                cache = await RetrieveAirlineFromDbAsync(code);

                //Key not in database, so get from Sabre.
                if (cache == null)
                {
                    cache = await _UtilityService.AirlineLookup(code);

                    //If get value, save into database
                    if (cache != null)
                    {
                        await SaveAirlineToDbAsync(cache.Code, cache.Name);
                    }
                }

                //Key has value, store in cache
                if (cache != null)
                {
                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                    // Save data in cache.
                    _cache.Set(key, cache, cacheEntryOptions);
                }
            }

            return cache;
        }
    }
}