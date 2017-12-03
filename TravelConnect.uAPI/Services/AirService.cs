using kestrel.SystemService;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;
using kestrel.AirService;
using TravelConnect.uAPI.Utility;
using TravelConnect.CommonServices;
using System.Linq;
using Newtonsoft.Json;

namespace TravelConnect.uAPI.Services
{
    public class AirService : IAirService
    {
        LogService _LogService;

        public async Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request)
        {
            AirLowFareSearchPortTypeClient client;
            _LogService = new LogService();

            try
            {
                _LogService.LogInfo($"FlightSearchRQ", request);

                var binding = GenerateBasicHttpBinding();

                var endpoint = new EndpointAddress("https://apac.universal-api.pp.travelport.com/B2BGateway/connect/uAPI/AirService");

                client = new AirLowFareSearchPortTypeClient(binding, endpoint);

                var httpHeaders = Helper.ReturnHttpHeader();
                client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

                var req = ConvertToLowFareSearchReq(request);
                _LogService.LogInfo($"uAPI/LowFareSearchReq", req);

                var result = await client.serviceAsync(null, req);
                _LogService.LogInfo($"uAPI/LowFareSearchRsp", result);

                var response = ConvertToFlightSearchRS(result.LowFareSearchRsp);
                _LogService.LogInfo($"FlightSearchRS", response);

                return response;
            }
            catch(Exception ex)
            {
                _LogService.LogException(ex, "uAPI.AirService.AirLowFareSearchAsyc");
                throw;
            }
            finally
            {
                client = null;
            }
        }

        public Task<List<string>> GetTopDestinationsAsync(string airportCode)
        {
            throw new NotImplementedException();
        }

        private typeSearchLocation GenerateTypeSearchLocation(string point, bool isAirport)
        {
            if (isAirport)
                return new typeSearchLocation { Item = new Airport { Code = point } };
            else
                return new typeSearchLocation { Item = new City { Code = point } };
        }

        private SearchAirLeg GenerateSearchAirLeg(string originPoint, bool isOriginAirport,
            string destinationPoint, bool isDestinationAirport, DateTime date)
        {
            return new SearchAirLeg
            {
                SearchOrigin = new typeSearchLocation[]
                    { GenerateTypeSearchLocation(originPoint, isOriginAirport) },
                SearchDestination = new typeSearchLocation[]
                    { GenerateTypeSearchLocation(destinationPoint, isDestinationAirport) },
                Items = new typeFlexibleTimeSpec[]
                    { new typeFlexibleTimeSpec { PreferredTime = date.ToString("yyyy-MM-dd") } }
            };
        }

        private BasicHttpsBinding GenerateBasicHttpBinding()
        {
            var binding = new BasicHttpsBinding();
            binding.Name = "SystemPingPort";
            binding.CloseTimeout = TimeSpan.FromMinutes(10);
            binding.OpenTimeout = TimeSpan.FromMinutes(10);
            binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            binding.SendTimeout = TimeSpan.FromMinutes(10);
            binding.Security.Mode = BasicHttpsSecurityMode.Transport;
            binding.MaxReceivedMessageSize = Int32.MaxValue;

            return binding;
        }

        private LowFareSearchReq ConvertToLowFareSearchReq(FlightSearchRQ request)
        {
            //Segments
            var SearchAirLegs = request.Segments.Select(s =>
                GenerateSearchAirLeg(s.Origin, true, s.Destination, true, s.Departure));

            AirSearchModifiers airSearchModifiers = new AirSearchModifiers
            { PreferredProviders = new Provider[] { new Provider { Code = "1G" } } };

            AirPricingModifiers airPricingModifiers = new AirPricingModifiers
            {
                ETicketabilitySpecified = true,
                ETicketability = typeEticketability.Required,
                FaresIndicatorSpecified = true,
                FaresIndicator = typeFaresIndicator.AllFares,
            };

            //Passengers
            var SearchPassengers = new List<SearchPassenger>();

            request.Ptcs.ForEach(p =>
            {
                for (int i = 1; i <= p.Quantity; i++)
                    SearchPassengers.Add(new SearchPassenger { Code = p.Code });
            });

            LowFareSearchReq req = new LowFareSearchReq
            {
                BillingPointOfSaleInfo = new kestrel.AirService.BillingPointOfSaleInfo
                {
                    OriginApplication = "uAPI"
                },
                TargetBranch = "P7073862",
                MaxNumberOfExpertSolutions = "50",
                SolutionResult = false,
                Items = SearchAirLegs.ToArray(),
                AirSearchModifiers = airSearchModifiers,
                AirPricingModifiers = airPricingModifiers,
                SearchPassenger = SearchPassengers.ToArray(),
                ReturnUpsellFare = true
            };

            return req;
        }

        //P0DT17H5M0S
        private int ToElapsed(string travelTime)
        {
            try
            {
                int idxP = travelTime.IndexOf("P");
                int idxDT = travelTime.IndexOf("DT");
                int idxH = travelTime.IndexOf("H");
                int idxM = travelTime.IndexOf("M");
                int idxS = travelTime.IndexOf("S");

                //_LogService.LogInfo($"taveltime: {travelTime}");
                //_LogService.LogInfo($"idxP: {idxP}");
                //_LogService.LogInfo($"idxH: {idxH}");
                //_LogService.LogInfo($"idxM: {idxM}");
                //_LogService.LogInfo($"Minute: {travelTime.Substring(idxH + 1, idxM - (idxH + 1))}");
                //_LogService.LogInfo($"Hour: {travelTime.Substring(idxDT + 2, idxH - (idxDT + 2))}");
                //_LogService.LogInfo($"Day: {travelTime.Substring(idxP + 1, idxDT - (idxP + 1))}");

                int elapsed = Convert.ToInt32(travelTime.Substring(idxH + 1, idxM - (idxH + 1)));
                elapsed += Convert.ToInt32(travelTime.Substring(idxDT + 2, idxH - (idxDT + 2))) * 60;
                elapsed += Convert.ToInt32(travelTime.Substring(idxP + 1, idxDT - (idxP + 1))) * 60 * 24;

                //Convert.ToInt32("sdfjh");

                return elapsed;
            }
            catch
            {
                _LogService.LogInfo($"Error convert travelTime {travelTime}");
                return 0;
            }
        }
        
        private void AllIndexOptions(ref List<List<int>> indexOptions, 
            List<FlightOption> flightOptions, List<int> index, int level)
        {
            //_LogService.LogInfo($"Level: {level}, Max: {flightOptions.Count}, Length: {flightOptions[level].Option.Count()}, index: {JsonConvert.SerializeObject(index)}");
            for (int idx=0; idx < flightOptions[level].Option.Length; idx++)
            {
                //_LogService.LogInfo($"idx: {idx}, index: {JsonConvert.SerializeObject(index)}");
                var localIndex = index.Select(i => i).ToList();

                localIndex.Add(idx);

                if (level == (flightOptions.Count - 1))
                {
                    indexOptions.Add(localIndex);
                }
                else
                {
                    int nextLevel = level + 1;
                    AllIndexOptions(ref indexOptions, flightOptions, localIndex, nextLevel);
                }
            }
        }

        private Models.Responses.Leg GenerateLeg(Option option, List<typeBaseAirSegment> segments)
        {
            Models.Responses.Leg leg = new Models.Responses.Leg
            {
                Elapsed = ToElapsed(option.TravelTime),
                Segments = option.BookingInfo.ToList().Select(bi => {
                    var segment = segments.Where(s => s.Key == bi.SegmentRef).FirstOrDefault();
                    return new SegmentRS
                    {
                        Origin = segment.Origin,
                        Destination = segment.Destination,
                        Departure = new Timing
                        {
                            Time = Convert.ToDateTime(segment.DepartureTime.Split('+')[0]),
                            GmtOffset = Convert.ToInt32(segment.DepartureTime.Split('+')[1].Split(':')[0])
                        },
                        Arrival = new Timing
                        {
                            Time = Convert.ToDateTime(segment.ArrivalTime.Split('+')[0]),
                            GmtOffset = Convert.ToInt32(segment.ArrivalTime.Split('+')[1].Split(':')[0])
                        },
                        BRD = bi.BookingCode,
                        Elapsed = Convert.ToInt32(segment.FlightTime),
                        MarketingFlight = new FlightNumber
                        {
                            Airline = segment.Carrier,
                            Number = segment.FlightNumber
                        },
                        OperatingFlight = new FlightNumber
                        {
                            Airline = segment.Carrier,
                            Number = segment.FlightNumber
                        }
                    };
                }).ToList()
            };

            return leg;
        }

        private FlightSearchRS ConvertToFlightSearchRS(LowFareSearchRsp response)
        {
            
            FlightSearchRS rs = new FlightSearchRS()
            {
                PricedItins = new List<PricedItin>(),
                RequestId = response.TransactionId,
            };

            ((AirPricePointList)response.Items.First()).AirPricePoint.ToList().ForEach(p =>
            {
                p.AirPricingInfo.ToList().ForEach(pi =>
                {
                    List<List<int>> options = new List<List<int>>();
                    //_LogService.LogInfo("FlightOptionsList: ", pi.FlightOptionsList);
                    AllIndexOptions(ref options, pi.FlightOptionsList.ToList(), 
                        new List<int>(), 0);
                    //_LogService.LogInfo("all options", options);

                    foreach(var leg in options)
                    {
                        List<Models.Responses.Leg> legs = new List<Models.Responses.Leg>();
                        for (int legIdx = 0; legIdx < leg.Count(); legIdx++)
                        {
                            legs.Add(GenerateLeg(pi.FlightOptionsList[legIdx].Option[leg[legIdx]], 
                                response.AirSegmentList.ToList()));
                        }

                        rs.PricedItins.Add(new PricedItin
                        {
                            TotalFare = new Fare
                            {
                                Amount = float.Parse(p.TotalPrice.Substring(3)),
                                Curr = p.TotalPrice.Substring(0, 3)
                            },
                            BaseFare = new Fare
                            {
                                Amount = float.Parse(p.BasePrice.Substring(3)),
                                Curr = p.BasePrice.Substring(0, 3)
                            },
                            Taxes = new Fare
                            {
                                Amount = float.Parse(p.Taxes.Substring(3)),
                                Curr = p.Taxes.Substring(0, 3)
                            },
                            Legs = legs
                        });
                    }

                    pi.FlightOptionsList.First().Option.ToList().ForEach(opt =>
                    {
                        
                    });
                    
                });
            });

            return rs;
        }
    }
}