using kestrel.SystemService;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;
using kestrel.AirService;
using TravelConnect.uAPI.Utility;
using TravelConnect.CommonServices;
using System.Linq;
using Newtonsoft.Json;

namespace TravelConnect.uAPI.Services
{
    public partial class AirService : IAirService
    {
        LogService _LogService;

        private enum JourneyType
        {
            DepartOnly = 0,
            ReturnOnly = 1,
            DepartAndReturn = 2,
        }

        private async Task<LowFareSearchRsp> SubmitAirLowFareSearchAsync(FlightSearchRQ request, JourneyType journeyType)
        {
            AirLowFareSearchPortTypeClient client;
            var binding = GenerateBasicHttpBinding();
            try
            {
                var endpoint = new EndpointAddress("https://apac.universal-api.pp.travelport.com/B2BGateway/connect/uAPI/AirService");

                client = new AirLowFareSearchPortTypeClient(binding, endpoint);

                var httpHeaders = Helper.ReturnHttpHeader();
                client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

                var req = ConvertToLowFareSearchReq(request, journeyType);
                _LogService.LogInfo($"uAPI/LowFareSearchReq_{journeyType}", req);

                var response = await client.serviceAsync(null, req);
                _LogService.LogInfo($"uAPI/LowFareSearchRsp_{journeyType}", response);
                return response.LowFareSearchRsp;
            }
            catch(Exception ex)
            {
                _LogService.LogException(ex, $"uAPI.AirService.SubmitAirLowFareSearchAsync_{journeyType}");
                return null;
            }
            finally
            {
                client = null;
                binding = null;
            }
        }

        public async Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request)
        {
            _LogService = new LogService();

            try
            {
                _LogService.LogInfo($"FlightSearchRQ", request);

                LowFareSearchRsp departAndReturnResult = null;
                LowFareSearchRsp departOnlyResult = null;
                LowFareSearchRsp returnOnlyResult = null;

                Task<LowFareSearchRsp> departAndReturnTask = null;
                Task<LowFareSearchRsp> returnOnlyTask = null;

                if (request.Segments.Count == 2)
                {
                    //Depart and Return Flight
                    departAndReturnTask = SubmitAirLowFareSearchAsync(request, JourneyType.DepartAndReturn);
                    
                    //Return Only Flight
                    returnOnlyTask = SubmitAirLowFareSearchAsync(request, JourneyType.ReturnOnly);
                }

                //Depart Only Flight
                Task<LowFareSearchRsp> departOnlyTask =
                    SubmitAirLowFareSearchAsync(request, JourneyType.DepartOnly);

                departOnlyResult = await departOnlyTask;
                
                if (request.Segments.Count == 2)
                {
                    departAndReturnResult = await departAndReturnTask;
                    returnOnlyResult = await returnOnlyTask;
                }
                
                var response = ConvertToFlightSearchRS(departAndReturnResult);
                _LogService.LogInfo($"FlightSearchRS", response);

                return response;
            }
            catch(Exception ex)
            {
                _LogService.LogException(ex, "uAPI.AirService.AirLowFareSearchAsyc");
                throw;
            }
            
        }

        public Task<List<string>> GetTopDestinationsAsync(string airportCode)
        {
            throw new NotImplementedException();
        }

        private typeSearchLocation GenerateTypeSearchLocation(string point, bool isAirport)
        {
            if (isAirport)
                return new typeSearchLocation { Item = new kestrel.AirService.Airport { Code = point } };
            else
                return new typeSearchLocation { Item = new kestrel.AirService.City { Code = point } };
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
            //binding.Name = "SystemPingPort";
            //binding.CloseTimeout = new TimeSpan(0, 3, 00);
            //binding.OpenTimeout = new TimeSpan(0, 3, 00);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 00);
            binding.SendTimeout = new TimeSpan(0, 10, 00);
            binding.Security.Mode = BasicHttpsSecurityMode.Transport;
            
            binding.MaxReceivedMessageSize = Int32.MaxValue;

            return binding;
        }

        private LowFareSearchReq ConvertToLowFareSearchReq(FlightSearchRQ request, JourneyType journeyType)
        {
            if (request.Segments.Count == 0)
                throw new  ApplicationException("Segment is required");
            //Segments
            IEnumerable<SearchAirLeg> SearchAirLegs = new List<SearchAirLeg>();

            switch (journeyType)
            {
                case JourneyType.DepartAndReturn:
                    if (request.Segments.Count == 1)
                        throw new ApplicationException("2 segment is request to generate Depart and Return journey");
                    SearchAirLegs = request.Segments.OrderBy(s => s.Departure).Select(s =>
                        GenerateSearchAirLeg(s.Origin, true, s.Destination, true, s.Departure));
                    break;
                case JourneyType.DepartOnly:
                    SearchAirLegs = request.Segments.OrderBy(s => s.Departure).Take(1).Select(s =>
                        GenerateSearchAirLeg(s.Origin, true, s.Destination, true, s.Departure));
                    break;
                case JourneyType.ReturnOnly:
                    if (request.Segments.Count == 1)
                        throw new ApplicationException("2 segment is request to generate Return Only journey");

                    SearchAirLegs = request.Segments.OrderBy(s => s.Departure).Skip(1).Take(1).Select(s =>
                        GenerateSearchAirLeg(s.Origin, true, s.Destination, true, s.Departure));
                    break;
            }
            

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
                    SearchPassengers.Add(new SearchPassenger
                    {
                        Code = p.Code == "CNN" ? "CHD" : p.Code,
                        Age = p.Code == "CNN" ? "8" : null,
                    });
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
            try
            {
                for (int idx = 0; idx < flightOptions[level].Option.Length; idx++)
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
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                var fullMethodName = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;
                
                _LogService.LogException(ex, fullMethodName);
                throw;
            }
        }

        private Timing ConvertToTiming(string datetime)
        {
            string[] date = datetime.Split("T");
            bool positiveOffset = date[1].IndexOf("+") > 0;
            string[] offset = date[1].Split(positiveOffset ? "+" : "-");

            Timing timing = new Timing();
            timing.Time = Convert.ToDateTime(date[0] + "T" + offset[0]);
            timing.GmtOffset = (Convert.ToInt32(offset[1].Split(':')[0]) 
                + (Convert.ToInt32(offset[1].Split(':')[1]) / 60))
                * (positiveOffset ? 1 : -1);

            return timing;
        }

        private Models.Responses.Leg GenerateLeg(Option option, List<typeBaseAirSegment> segments)
        {
            typeBaseAirSegment segment = new typeBaseAirSegment();
            try
            {
                Models.Responses.Leg leg = new Models.Responses.Leg
                {
                    Elapsed = ToElapsed(option.TravelTime),
                    Segments = option.BookingInfo.ToList().Select(bi =>
                    {
                        segment = segments.Where(s => s.Key == bi.SegmentRef).FirstOrDefault();
                        return new SegmentRS
                        {
                            Key = segment.Key,
                            Group = segment.Group,
                            Origin = segment.Origin,
                            Destination = segment.Destination,
                            Departure = ConvertToTiming(segment.DepartureTime),
                            Arrival = ConvertToTiming(segment.ArrivalTime),
                            BookingCode = bi.BookingCode,
                            CabinClass = bi.CabinClass,
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
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                var fullMethodName = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;

                _LogService.LogInfo($"currentMethod.Name/Segment", segment);
                _LogService.LogException(ex, fullMethodName);
                throw;
            }
        }

        private List<Models.Responses.FareInfo> GenerateFareInfos(List<List<FareInfoRef>> fareInfoRefs,
            List<kestrel.AirService.FareInfo> fareInfos, List<kestrel.AirService.Brand> brands)
        {
            try
            {
                List<Models.Responses.FareInfo> response = new List<Models.Responses.FareInfo>();


                fareInfoRefs.ForEach(fInfo =>
                {
                    Models.Responses.FareInfo fareInfoRs = new Models.Responses.FareInfo()
                    {
                        FareInfoDetails = new List<FareInfoDetail>()
                    };

                    fInfo.ForEach(fi =>
                    {
                        kestrel.AirService.FareInfo fareInfo = fareInfos.First(f => f.Key == fi.Key);
                        
                        kestrel.AirService.Brand brand = null;
                        if (fareInfo.Brand != null)
                            brand = brands.FirstOrDefault(b => b.BrandID == fareInfo.Brand.BrandID);

                        fareInfoRs.Ptc = fareInfo.PassengerTypeCode;

                        fareInfoRs.FareInfoDetails.Add(new Models.Responses.FareInfoDetail
                        {
                            FareBasis = fareInfo.FareBasis,
                            Origin = fareInfo.Origin,
                            Destination = fareInfo.Destination,
                            IsPrivateFare = fareInfo.PrivateFareSpecified,
                            Amount = new Fare
                            {
                                Curr = fareInfo.Amount.Substring(0, 3),
                                Amount = Convert.ToInt32(fareInfo.Amount.Substring(3))
                            },
                            Brand = (brand == null || fareInfo.Brand == null) ? null
                                : new Models.Responses.Brand
                                {
                                    Key = brand.Key,
                                    Carrier = brand.Carrier,
                                    BrandId = fareInfo.Brand.BrandID,
                                    Name = brand.Name,
                                    UpsellBrandFound = fareInfo.Brand.UpSellBrandFound
                                }
                        });
                    });

                    response.Add(fareInfoRs);
                });
                
                return response;
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                var fullMethodName = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;

                _LogService.LogException(ex, fullMethodName);
                throw;
            }
        }


        private FlightSearchRS ConvertToFlightSearchRS(LowFareSearchRsp response)
        {
            
            FlightSearchRS rs = new FlightSearchRS()
            {
                PricedItins = new List<PricedItin>(),
                RequestId = response.TransactionId,
            };

            try
            {
                foreach (AirPricePoint p in ((AirPricePointList)response.Items.First()).AirPricePoint)
                {
                    List<List<int>> options = new List<List<int>>();
                    //_LogService.LogInfo("FlightOptionsList: ", pi.FlightOptionsList);
                    AllIndexOptions(ref options, p.AirPricingInfo.First().FlightOptionsList.ToList(),
                        new List<int>(), 0);
                    //_LogService.LogInfo("all options", options);

                    foreach (var leg in options)
                    {
                        List<Models.Responses.Leg> legs = new List<Models.Responses.Leg>();
                        for (int legIdx = 0; legIdx < leg.Count(); legIdx++)
                        {
                            legs.Add(GenerateLeg(p.AirPricingInfo.First().FlightOptionsList[legIdx].Option[leg[legIdx]],
                                response.AirSegmentList.ToList()));
                        }

                        var a = p.AirPricingInfo.ToList().Select(ap => ap.FareInfoRef.ToList());

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
                            Legs = legs,
                            FareInfos = GenerateFareInfos(
                                p.AirPricingInfo.ToList().Select(ap => ap.FareInfoRef.ToList()).ToList(),
                                response.FareInfoList.ToList(),
                                response.BrandList?.ToList() ?? new List<kestrel.AirService.Brand>())
                        });

                        p.AirPricingInfo.ToList().ForEach(pi =>
                        {

                        });
                    }
                };

                return rs;
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "uAPI.AirService.ConvertToFlightSearchRS");
                return null;
            }
        }
    }
}