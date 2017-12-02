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

namespace TravelConnect.uAPI.Services
{
    public class AirService : IAirService
    {
        public Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request)
        {
            LogService _LogService = new LogService();

            _LogService.LogInfo($"FlightSearchRQ", request);

            var binding = GenerateBasicHttpBinding();

            var endpoint = new EndpointAddress("https://apac.universal-api.pp.travelport.com/B2BGateway/connect/uAPI/AirService");

            AirLowFareSearchPortTypeClient client = new AirLowFareSearchPortTypeClient(binding, endpoint);

            var httpHeaders = Helper.ReturnHttpHeader();
            client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

            var req = ConvertToLowFareSearchReq(request);
            
            _LogService.LogInfo($"uAPI/LowFareSearchReq", req);

            var result = client.serviceAsync(null, req).Result;

            _LogService.LogInfo($"uAPI/LowFareSearchRsp", result);

            return null;
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
            binding.CloseTimeout = TimeSpan.FromMinutes(1);
            binding.OpenTimeout = TimeSpan.FromMinutes(1);
            binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            binding.SendTimeout = TimeSpan.FromMinutes(1);
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
    }
}