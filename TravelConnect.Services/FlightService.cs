using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Interfaces.Models;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre;

namespace TravelConnect.Services
{
    public class FlightService : IFlightService
    {
        private ISabreConnector _SabreConnector;
        private IMemoryCache _cache;
        

        public FlightService(ISabreConnector _SabreConnector, IMemoryCache memoryCache)
        {
            this._SabreConnector = _SabreConnector;
            this._cache = memoryCache;
        }

        private async Task<SearchRS> GetAirLowFareSearch(SearchRQ request)
        {
            string result = await
            _SabreConnector.SendRequestAsync("/v3.2.0/shop/flights?mode=live&limit=200&offset=1",
                JsonConvert.SerializeObject(request.AirLowFareSearchRQ(),
                    Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatString = "yyyy-MM-ddTHH:mm:ss"
                    }), true);

            AirLowFareSearchRS rs =
            JsonConvert.DeserializeObject<AirLowFareSearchRS>(result);

            return ConvertToSearchRS(rs);
        }

        public async Task<SearchRS> AirLowFareSearch(SearchRQ request)
        {
            string sRequest = JsonConvert.SerializeObject(request);
            SearchRS cacheSearchRS;

            if (!_cache.TryGetValue(sRequest, out cacheSearchRS))
            {
                // Key not in cache, so get data.
                cacheSearchRS = await GetAirLowFareSearch(request);
                
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                // Save data in cache.
                _cache.Set(sRequest, cacheSearchRS, cacheEntryOptions);
            }

            return cacheSearchRS;
        }

        private SearchRS ConvertToSearchRS(AirLowFareSearchRS airlowFare)
        {
            if (airlowFare == null)
                return null;

            SearchRS rs = new SearchRS
            {
                PricedItins = airlowFare.OTA_AirLowFareSearchRS
                    .PricedItineraries.PricedItinerary.Select(a =>
                    {
                        var TotalFare = a.AirItineraryPricingInfo.Select(pi => pi.ItinTotalFare.TotalFare).FirstOrDefault();
                        if (TotalFare != null)
                        {
                            return new PricedItin
                            {
                                Curr = TotalFare.CurrencyCode,
                                TotalPrice = TotalFare.Amount,
                                Legs = a.AirItinerary.OriginDestinationOptions
                                    .OriginDestinationOption.Select(dest => {
                                        return new Leg
                                        {
                                            Elapsed = dest.ElapsedTime,
                                            Segments = dest.FlightSegment.Select(seg =>
                                            {
                                                return new Interfaces.Models.Segment
                                                {
                                                     Origin = seg.DepartureAirport.LocationCode,
                                                     Destination = seg.ArrivalAirport.LocationCode,
                                                     Elapsed = seg.ElapsedTime,
                                                     Departure = new Timing
                                                     {
                                                         Time = seg.DepartureDateTime,
                                                         GmtOffset = seg.DepartureTimeZone.GMTOffset,
                                                     },
                                                     Arrival = new Timing
                                                     {
                                                         Time = seg.ArrivalDateTime,
                                                         GmtOffset = seg.ArrivalTimeZone.GMTOffset
                                                     },
                                                     MarketingFlight = new FlightNumber
                                                     {
                                                         Airline = seg.MarketingAirline.Code,
                                                         Number = seg.FlightNumber
                                                     },
                                                     OperatingFlight = new FlightNumber
                                                     {
                                                         Airline = seg.OperatingAirline.Code,
                                                         Number = seg.OperatingAirline.FlightNumber
                                                     }
                                                };
                                            }).ToList()
                                        };
                                    }).ToList()
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }).ToList()
            };

            return rs;
        }
    }
}