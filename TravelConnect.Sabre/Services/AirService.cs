using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre.Interfaces;
using TravelConnect.Sabre.Models;

namespace TravelConnect.Sabre.Services
{
    public class AirService : IAirService
    {
        private ISabreConnector _SabreConnector;

        public AirService(ISabreConnector _SabreConnector)
        {
            this._SabreConnector = _SabreConnector;
        }

        public async Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request)
        {
            try
            {
                string result = await
                    _SabreConnector.SendRequestAsync("/v3.2.0/shop/flights?mode=live&limit=200&offset=1&enabletagging=true",
                        JsonConvert.SerializeObject(ConvertToAirLowFareSearchRQ(request),
                            Formatting.None, new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                DateFormatString = "yyyy-MM-ddTHH:mm:ss"
                            }), true);

                AirLowFareSearchRS rs = JsonConvert.DeserializeObject<AirLowFareSearchRS>(result);

                return ConvertToSearchRS(rs);
            }
            catch
            {
                return null;
            }
        }

        public Task<AirPriceRS> AirPriceAsync(AirPriceRQ request)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<string>> GetTopDestinationsAsync(string airportCode)
        {
            throw new System.NotImplementedException();
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
                                    new TravelConnect.Models.Requests.Passengertypequantity
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
                TPA_Extensions = new TravelConnect.Models.Requests.TPA_Extensions2
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

        private FlightSearchRS ConvertToSearchRS(AirLowFareSearchRS airlowFare)
        {
            if (airlowFare?.Page == null)
                return null;

            FlightSearchRS rs = new FlightSearchRS
            {
                RequestId = airlowFare.RequestId,
                Page = new TravelConnect.Models.Responses.Page
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
                                                    BookingCode = seg.ResBookDesigCode,
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
    }
}