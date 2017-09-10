using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
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

        private async Task<FlightSearchRS> GetAirLowFareSearch(FlightSearchRQ request)
        {
            string result = await
            _SabreConnector.SendRequestAsync("/v3.2.0/shop/flights?mode=live&limit=200&offset=1",
                JsonConvert.SerializeObject(ConvertToAirLowFareSearchRQ(request),
                    Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatString = "yyyy-MM-ddTHH:mm:ss"
                    }), true);

            AirLowFareSearchRS rs =
            JsonConvert.DeserializeObject<AirLowFareSearchRS>(result);

            return ConvertToSearchRS(rs);
        }
        
        public async Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request)
        {
            string sRequest = JsonConvert.SerializeObject(request);
            FlightSearchRS cacheSearchRS;

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

        private FlightSearchRS ConvertToSearchRS(AirLowFareSearchRS airlowFare)
        {
            if (airlowFare == null)
                return null;

            FlightSearchRS rs = new FlightSearchRS
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
                                                return new SegmentRS
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

        private AirLowFareSearchRQ ConvertToAirLowFareSearchRQ(FlightSearchRQ request)
        {

            AirLowFareSearchRQ rq = new AirLowFareSearchRQ();
            int segmentIndex = 1;
            rq.OTA_AirLowFareSearchRQ = new OTA_Airlowfaresearchrq
            {
                AvailableFlightsOnly = request.AvailableFlightsOnly,
                DirectFlightsOnly = request.DirectFlightsOnly,
                Target = "Production",
                POS = new POS
                {
                    Source = new Source[]
                    {
                            new Source
                            {
                                PseudoCityCode = "F9CE",
                                RequestorID = new Requestorid
                                {
                                    Type = "1",
                                    ID = "1",
                                    CompanyName = new Companyname()
                                }
                            }
                    }
                },
                OriginDestinationInformation = request.Segments.Select(s =>
                    new Origindestinationinformation
                    {
                        RPH = (segmentIndex++).ToString(),
                        DepartureDateTime = s.Departure,
                        OriginLocation = new Originlocation
                        {
                            LocationCode = s.Origin
                        },
                        DestinationLocation = new Destinationlocation
                        {
                            LocationCode = s.Destination
                        }
                    }).ToArray(),
                TravelerInfoSummary = new Travelerinfosummary
                {
                    SeatsRequested = new int[] { 1 },
                    AirTravelerAvail = new Airtraveleravail[]
                    {
                            new Airtraveleravail
                            {
                                PassengerTypeQuantity = request.Ptcs.Select(p =>
                                    new Models.Requests.Passengertypequantity
                                    {
                                        Code = p.Code,
                                        Quantity = p.Quantity,
                                        Changeable = false
                                    }
                                ).ToArray()
                            }
                    }
                },
                TravelPreferences = new Travelpreferences
                {
                    CabinPref = new Cabinpref[]
                    {
                            new Cabinpref
                            {
                                Cabin = "Y",
                                PreferLevel = "Preferred"
                            }
                    },
                    VendorPref = new Vendorpref[]
                    {
                            new Vendorpref
                            {
                                Code = "3K",
                                PreferLevel = "Unacceptable"
                            }
                    }
                },
                TPA_Extensions = new Models.Requests.TPA_Extensions2
                {
                    IntelliSellTransaction = new Intelliselltransaction
                    {
                        RequestType = new Requesttype
                        {
                            Name = "200ITINS"
                        }
                    }
                }
            };

            return rq;

        }
    }
}