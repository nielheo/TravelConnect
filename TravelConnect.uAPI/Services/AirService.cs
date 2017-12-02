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

namespace TravelConnect.uAPI.Services
{
    public class AirService : IAirService
    {
        public Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request)
        {
            SubmitAirLowFareSearchRequest();

            return null;
        }

        private void SubmitAirLowFareSearchRequest()
        {
            var binding = new BasicHttpsBinding();
            binding.Name = "SystemPingPort";
            binding.CloseTimeout = TimeSpan.FromMinutes(1);
            binding.OpenTimeout = TimeSpan.FromMinutes(1);
            binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            binding.SendTimeout = TimeSpan.FromMinutes(1);
            binding.Security.Mode = BasicHttpsSecurityMode.Transport;
            //binding.ProxyAddress = new Uri("http://localhost:8888");
            //BasicHttpSecurityMode.Transport;

            //            binding.Security.Transport.

            var endpoint = new EndpointAddress("https://apac.universal-api.pp.travelport.com/B2BGateway/connect/uAPI/AirService");

            AirLowFareSearchPortTypeClient client = new AirLowFareSearchPortTypeClient(binding, endpoint);

            //client.ClientCredentials.UserName.UserName = "UniversalAPI/uAPI8931078193-41fe5ac8";
            //client.ClientCredentials.UserName.Password = "kE8jAwj28td8nqQTSgtM2rhw7";

            var httpHeaders = Helper.ReturnHttpHeader();
            client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

            LowFareSearchReq req = new LowFareSearchReq
            {

            };

            var result = client.serviceAsync(null, req).Result;
        }

        public Task<List<string>> GetTopDestinationsAsync(string airportCode)
        {
            throw new NotImplementedException();
        }
    }
}