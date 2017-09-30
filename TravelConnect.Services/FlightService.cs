using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre;
using TravelConnect.Sabre.Models;

namespace TravelConnect.Services
{
    public class FlightService : IFlightService
    {
        private ISabreConnector _SabreConnector;
        private IMemoryCache _cache;
        private readonly TCContext _context;

        public FlightService(ISabreConnector _SabreConnector, 
            IMemoryCache memoryCache, TCContext _context)
        {
            this._SabreConnector = _SabreConnector;
            this._cache = memoryCache;
            this._context = _context;
        }

        private async Task<FlightSearchRS> SubmitAirLowFareSearch(FlightSearchRQ request)
        {
            try
            {
                //string r = JsonConvert.SerializeObject(ConvertToAirLowFareSearchRQ(request),
                //        Formatting.None, new JsonSerializerSettings
                //        {
                //            NullValueHandling = NullValueHandling.Ignore,
                //            DateFormatString = "yyyy-MM-ddTHH:mm:ss"
                //        });

                string result = await
                    _SabreConnector.SendRequestAsync("/v3.2.0/shop/flights?mode=live&limit=200&offset=1&enabletagging=true",
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
            catch
            {
                return null;
            }
        }

        private async Task<FlightSearchRS> SubmitNextAirLowFareSearch(string requestId, int page)
        {
            string result = await
                _SabreConnector.SendRequestAsync(
                    string.Format("/v3.2.0/shop/flights/{0}", requestId),
                    string.Format("mode=live&limit=200&offset={0}", page), false);

            AirLowFareSearchRS rs = JsonConvert.DeserializeObject<AirLowFareSearchRS>(result);

            return ConvertToSearchRS(rs);
        }

        private async Task<FlightSearchRS> RetrieveAirLowFareSearch(FlightSearchRQ request)
        {
            string sRequest = "AirLowFare_" + JsonConvert.SerializeObject(request);
            FlightSearchRS cacheSearchRS;

            if (!_cache.TryGetValue(sRequest, out cacheSearchRS))
            {
                // Key not in cache, so get data.
                cacheSearchRS = await SubmitAirLowFareSearch(request);

                if (cacheSearchRS == null)
                {
                    cacheSearchRS = new FlightSearchRS
                    {
                        PricedItins = new List<PricedItin>()
                    };
                }

                // Set cache options.

                if (cacheSearchRS != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromHours(1));
                    
                    // Save data in cache.
                    _cache.Set(sRequest, cacheSearchRS, cacheEntryOptions);
                }
            }

            return cacheSearchRS;
        }

        private List<string> GetAirlines(FlightSearchRS rs)
        {
            if (rs == null)
                return new List<string>();
            return rs.PricedItins.SelectMany(p => p.Legs.SelectMany(l => l.Segments.Select(s=>s.MarketingFlight.Airline)))
                .Distinct().ToList();
        }

        public async Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request)
        {
            FlightSearchRS rs = await RetrieveAirLowFareSearch(request);

            if (request.Segments.Count == 1 || (request.Airlines?.Count ?? 0) > 0)
            {
                rs.Airlines = GetAirlines(rs);
            } else
            {
                request.Segments = new List<SegmentRQ>
                {
                    request.Segments.First()
                };
                FlightSearchRS ow = await RetrieveAirLowFareSearch(request);
                rs.Airlines = ((ow?.PricedItins?.Count ?? 0) > 0) 
                    ? GetAirlines(ow)
                    : GetAirlines(rs);
            }

            return rs;
        }

        private FlightSearchRS ConvertToSearchRS(AirLowFareSearchRS airlowFare)
        {
            if (airlowFare?.Page == null)
                return null;

            FlightSearchRS rs = new FlightSearchRS
            {
                RequestId = airlowFare.RequestId,
                Page = new Models.Responses.Page
                {
                     Size = airlowFare.Page.Size,
                     Offset = airlowFare.Page.Offset,
                     TotalTags = airlowFare.Page.TotalTags
                },
                PricedItins = airlowFare.OTA_AirLowFareSearchRS
                    .PricedItineraries.PricedItinerary.Select(a =>
                    {
                        var ItinTotalFare = a.AirItineraryPricingInfo.Select(pi => pi.ItinTotalFare).FirstOrDefault();
                        var TotalFare = a.AirItineraryPricingInfo.Select(pi => pi.ItinTotalFare.TotalFare).FirstOrDefault();
                        var BaseFare = a.AirItineraryPricingInfo.Select(pi => pi.ItinTotalFare.BaseFare).FirstOrDefault();
                        var Taxes = a.AirItineraryPricingInfo.Select(pi => pi.ItinTotalFare.Taxes).FirstOrDefault();


                        if (TotalFare != null)
                        {
                            return new PricedItin
                            {
                                TotalFare = new Fare
                                {
                                    Curr = TotalFare.CurrencyCode,
                                    Amount = TotalFare.Amount
                                },
                                BaseFare = new Fare
                                {
                                    Curr = BaseFare.CurrencyCode,
                                    Amount = BaseFare.Amount
                                },
                                Taxes = new Fare
                                {
                                    Curr = Taxes.Tax[0].CurrencyCode,
                                    Amount = Taxes.Tax[0].Amount
                                },
                                LastTicketDate = a.AirItineraryPricingInfo.FirstOrDefault()?.LastTicketDate,
                                Legs = a.AirItinerary.OriginDestinationOptions
                                    .OriginDestinationOption.Select(dest =>
                                    {
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
                                                    },
                                                    BRD = seg.ResBookDesigCode,
                                                    MarriageGrp = seg.MarriageGrp
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
            if (request.Airlines == null)
                request.Airlines = new List<string>();
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
                    },
                     
                    //PriceRequestInformation = new Pricerequestinformation
                    //{
                    //    CurrencyCode = "THB",
                    ////    NegotiatedFaresOnly = true,
                    //}
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
                    VendorPref = request.Airlines.Select(air => new Vendorpref
                    {
                        Code = air.ToUpper(),
                        PreferLevel = "Preferred"
                    }).ToArray()
                    //VendorPref = new Vendorpref[]
                    //{
                    //        new Vendorpref
                    //        {
                    //            Code = "AF",
                    //            PreferLevel = "Preferred"
                    //        }
                    //}
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

        private async Task<Models.Responses.AirlineRS> SubmitGetAirlineAsync(string code)
        {
            try
            {
                var result = await _SabreConnector.SendRequestAsync("/v1/lists/utilities/airlines",
                    "airlinecode=" + code, false);


                Sabre.Models.AirlineRS rs = JsonConvert.DeserializeObject<Sabre.Models.AirlineRS>(result);

                if (rs.AirlineInfo?.FirstOrDefault() == null)
                    return null;

                return new Models.Responses.AirlineRS
                {
                    Code = rs.AirlineInfo.FirstOrDefault().AirlineCode,
                    Name = rs.AirlineInfo.FirstOrDefault().AirlineName
                };
            }
            catch
            {
                return null;
            }
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

        private async Task<Models.Responses.AirlineRS> SaveAirlineToDbAsync(string id, string name)
        {
            _context.Airlines.Add(new Airline
            {
                Id = id,
                Name = name
            });

            int x = await _context.SaveChangesAsync();
            
            return new Models.Responses.AirlineRS
            {
                Code = id,
                Name = name
            };
        }


        public async Task<Models.Responses.AirlineRS> GetAirlineAsync(string code)
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
                    cache = await SubmitGetAirlineAsync(code);

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
                        .SetSlidingExpiration(TimeSpan.FromHours(1));

                    // Save data in cache.
                    _cache.Set(key, cache, cacheEntryOptions);
                }
            }

            return cache;
        }

        public async Task<FlightSearchRS> NextAirLowFareSearchAsync(string requestId, int page)
        {
            string key = string.Format("nextAirLowFare_{0}_{1}", requestId, page);
            FlightSearchRS cacheSearchRS;

            if (!_cache.TryGetValue(key, out cacheSearchRS))
            {
                // Key not in cache, so get data.
                cacheSearchRS = await SubmitNextAirLowFareSearch(requestId, page);

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