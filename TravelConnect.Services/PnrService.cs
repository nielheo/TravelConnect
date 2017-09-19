using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.CreatePnr;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre;
using TravelConnect.Sabre.Models.Pnr;
using TravelConnect.Sabre.Models.PnrResponse;

namespace TravelConnect.Services
{
    public class PnrService : IPnrService
    {
        private ISabreConnector _SabreConnector;
        
        public PnrService(ISabreConnector _SabreConnector)
        {
            this._SabreConnector = _SabreConnector;
        }

        public async Task<CreatePnrRS> CreatePnrAsync(CreatePnrRQ request)
        {
            try
            {
                string r = JsonConvert.SerializeObject(ConvertToCreatePassNamenRecordRQ(request),
                        Formatting.None, new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            DateFormatString = "yyyy-MM-ddTHH:mm:ss"
                        });

                string result = await
                    _SabreConnector.SendRequestAsync("/v2.0.0/passenger/records?mode=create", r, true);
                
                CreatePassNameRecordRS rs = 
                    JsonConvert.DeserializeObject<CreatePassNameRecordRS>(result);

                return ConvertToCreatePnrRS(rs);
            }
            catch
            {
                return new CreatePnrRS
                {
                    Status = "Failed"
                };
            }
        }

        private CreatePassNameRecordRQ ConvertToCreatePassNamenRecordRQ(CreatePnrRQ request)
        {
            CreatePassNameRecordRQ rq = new CreatePassNameRecordRQ()
            {
                CreatePassengerNameRecordRQ = new Createpassengernamerecordrq()
            };

            if ((request.AirSegments?.Count ?? 0) > 0)
            {
                rq.CreatePassengerNameRecordRQ.AirBook = new Sabre.Models.Pnr.Airbook
                {
                    OriginDestinationInformation = new Origindestinationinformation
                    {
                        FlightSegment = request.AirSegments.Select(a => new Sabre.Models.Pnr.Flightsegment
                        {
                            DepartureDateTime = a.Departure,
                            ArrivalDateTime = a.Arrival,
                            FlightNumber = a.MarketingAirline.Number,
                            NumberInParty = request.Passengers.Count,
                            ResBookDesigCode = a.Brd,
                            Status = "NN",
                            OriginLocation = new Sabre.Models.Pnr.Originlocation
                            {
                                LocationCode = a.Origin
                            },
                            DestinationLocation = new Sabre.Models.Pnr.Destinationlocation
                            {
                                LocationCode = a.Destination
                            },
                            MarketingAirline = new Sabre.Models.Pnr.Marketingairline
                            {
                                Code = a.MarketingAirline.Code,
                                FlightNumber = a.MarketingAirline.Number
                            },
                            MarriageGrp = a.MarriageGrp
                        }).ToArray()
                    }
                };
            }

            rq.CreatePassengerNameRecordRQ.AirPrice = new Airprice
            {
                PriceRequestInformation = new Pricerequestinformation
                {
                    OptionalQualifiers = new Optionalqualifiers
                    {
                        PricingQualifiers = new Pricingqualifiers
                        {
                            PassengerType = request.Passengers.Select(p => p.Type).Distinct()
                            .Select(p =>
                                new Passengertype
                                {
                                    Code = p,
                                    Quantity = request.Passengers.Where(ps=>ps.Type == p).Count()
                                }
                            ).ToArray()
                        }
                    }
                }
            };

            rq.CreatePassengerNameRecordRQ.PostProcessing = new Postprocessing
            {
                EndTransaction = new Endtransaction
                {
                    Source = new Sabre.Models.Pnr.Source
                    {
                        ReceivedFrom = "SWS TEST"
                    }
                }
            };

            return rq;
        }

        private CreatePnrRS ConvertToCreatePnrRS(CreatePassNameRecordRS response)
        {
            CreatePnrRS rs = new CreatePnrRS
            {
                RecordLocator = response.CreatePassengerNameRecordRS.ItineraryRef.ID,
            };

            return rs;
        }
    }
}