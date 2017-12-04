using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;
using kestrel.AirService;
using TravelConnect.CommonServices;
using TravelConnect.uAPI.Utility;
using System.Linq;

namespace TravelConnect.uAPI.Services
{
    public partial class AirService : IAirService
    {
        public async Task<AirPriceRS> AirPriceAsync(AirPriceRQ request)
        {
            AirPricePortTypeClient client;
            _LogService = new LogService();

            try
            {
                _LogService.LogInfo($"AirPriceRQ", request);

                var binding = GenerateBasicHttpBinding();

                var endpoint = new EndpointAddress("https://apac.universal-api.pp.travelport.com/B2BGateway/connect/uAPI/AirService");

                client = new AirPricePortTypeClient(binding, endpoint);

                var httpHeaders = Helper.ReturnHttpHeader();
                client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

                var req = ConvertToAirPriceReq(request);
                _LogService.LogInfo($"uAPI/AirPriceReq", req);

                var result = await client.serviceAsync(null, req);
                _LogService.LogInfo($"uAPI/AirPriceRsp", result);

                var response = ConvertToAirPriceRS(result.AirPriceRsp);
                _LogService.LogInfo($"AirPriceRS", response);

                return response;
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "uAPI.AirService.AirPriceAsync");
                throw;
            }
            finally
            {
                client = null;
            }
        }

        private AirPriceReq ConvertToAirPriceReq(AirPriceRQ request)
        {
            AirPriceReq req = new AirPriceReq()
            {
                BillingPointOfSaleInfo = new kestrel.AirService.BillingPointOfSaleInfo
                {
                    OriginApplication = "uAPI"
                },
                TargetBranch = "P7073862",
                AirItinerary = new AirItinerary
                {
                    AirSegment = request.Segments.Select(s => new typeBaseAirSegment
                    {
                        Key = s.Key,
                        Group = s.Group,
                        Origin = s.Origin,
                        Destination = s.Destination,
                        FlightNumber = s.FlightNumber.Number,
                        Carrier = s.FlightNumber.Airline,
                        FlightDetails = s.FlightDetails.Select(fd => new FlightDetails
                        {
                            DepartureTime = fd.DepartureTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            ArrivalTime = fd.ArrivalTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        }).ToArray()
                    }).ToArray()
                }
            };

            return req;
        }

        private AirPriceRS ConvertToAirPriceRS(AirPriceRsp response)
        {
            AirPriceRS rs = new AirPriceRS();

            return rs;
        }
    }
}