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
using System.Collections.Generic;

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
            int counter = 0;
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
                        FlightNumber = s.FlightNumber.Number,
                        Carrier = s.FlightNumber.Airline,
                        Origin = s.Origin,
                        Destination = s.Destination,
                        ProviderCode = "1G",
                        DepartureTime = $"{s.DepartureTime.Time.ToString("yyyy-MM-ddTHH:mm:ss.000")}+{s.DepartureTime.GmtOffset.ToString("00")}:00",
                        ArrivalTime = $"{s.ArrivalTime.Time.ToString("yyyy-MM-ddTHH:mm:ss.000")}+{s.ArrivalTime.GmtOffset.ToString("00")}:00",
                        ClassOfService = s.ClassOfService,
                        Connection = s.IsConnection ? new Connection { } : null
                    }).ToArray()
                },
                SearchPassenger = request.Ptcs.Select(ptc =>
                {
                    SearchPassenger pax = new SearchPassenger
                    {
                        Code = ptc.Code,
                        BookingTravelerRef = $"PT{counter++}",
                    };
                    if (ptc.Age != null) pax.Age = ptc.Age.ToString();
                    return pax;
                }).ToArray(),
                AirPricingCommand = new AirPricingCommand[]
                {
                    new AirPricingCommand
                    {
                         CabinClass = request.CabinClass,
                    }
                },
               
                //AirPricingModifiers = new AirPricingModifiers
                //{
                //    BrandModifiers = new BrandModifiers
                //    {
                //        ModifierType = BrandModifiersModifierType.FareFamilyDisplay
                //    }
                //}
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