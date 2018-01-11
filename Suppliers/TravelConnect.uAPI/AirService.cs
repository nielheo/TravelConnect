using kestrel.SystemService;
using System;
using System.ServiceModel;
using TravelConnect.uAPI.Utility;

namespace TravelConnect.uAPI
{
    public class AirService
    {
        public void Ping()
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

            var endpoint = new EndpointAddress("https://apac.universal-api.pp.travelport.com/B2BGateway/connect/uAPI/SystemService");

            SystemPingPortTypeClient client = new SystemPingPortTypeClient(binding, endpoint);

            //client.ClientCredentials.UserName.UserName = "UniversalAPI/uAPI8931078193-41fe5ac8";
            //client.ClientCredentials.UserName.Password = "kE8jAwj28td8nqQTSgtM2rhw7";

            var httpHeaders = Helper.ReturnHttpHeader();
            client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

            PingReq req = new PingReq
            {
                TraceId = "Test",
                Payload = "Ping test",
            };

            var result = client.serviceAsync(req).Result;
        }
    }
}