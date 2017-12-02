using kestrel.AirService;
using kestrel.SystemService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using TravelConnect.CommonServices;
using TravelConnect.uAPI.Utility;

namespace uAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var binding = new BasicHttpsBinding();
            binding.Name = "SystemPingPort";
            binding.CloseTimeout = TimeSpan.FromMinutes(1);
            binding.OpenTimeout = TimeSpan.FromMinutes(1);
            binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            binding.SendTimeout = TimeSpan.FromMinutes(1);
            binding.Security.Mode = BasicHttpsSecurityMode.Transport;
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            
            //binding.ProxyAddress = new Uri("http://localhost:8888");
            //BasicHttpSecurityMode.Transport;

            //            binding.Security.Transport.

            var endpoint = new EndpointAddress("https://apac.universal-api.pp.travelport.com/B2BGateway/connect/uAPI/AirService");

            AirLowFareSearchPortTypeClient client = new AirLowFareSearchPortTypeClient(binding, endpoint);

            //client.ClientCredentials.UserName.UserName = "UniversalAPI/uAPI8931078193-41fe5ac8";
            //client.ClientCredentials.UserName.Password = "kE8jAwj28td8nqQTSgtM2rhw7";

            var httpHeaders = Helper.ReturnHttpHeader();
            client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

            var SearchAirLegs = new List<SearchAirLeg>();
            SearchAirLegs.Add(new SearchAirLeg
            {
                SearchOrigin = new typeSearchLocation[]
                {
                    new typeSearchLocation
                    {
                        Item = new Airport { Code = "BKK" }
                    }
                },
                SearchDestination = new typeSearchLocation[]
                {
                    new typeSearchLocation
                    {
                        Item = new Airport { Code = "SIN" }
                    }
                },
                Items = new typeFlexibleTimeSpec[]
                {
                    new typeFlexibleTimeSpec
                    {
                        PreferredTime = DateTime.Today.AddDays(180).ToString("yyyy-MM-dd")
                    }
                }
            });

            SearchAirLegs.Add(new SearchAirLeg
            {
                SearchOrigin = new typeSearchLocation[]
                {
                    new typeSearchLocation
                    {
                        Item = new Airport { Code = "SIN" }
                    }
                },
                SearchDestination = new typeSearchLocation[]
                {
                    new typeSearchLocation
                    {
                        Item = new Airport { Code = "BKK" }
                    }
                },
                Items = new typeFlexibleTimeSpec[]
                {
                    new typeFlexibleTimeSpec
                    {
                        PreferredTime = DateTime.Today.AddDays(185).ToString("yyyy-MM-dd")
                    }
                }
            });

            AirSearchModifiers airSearchModifiers = new AirSearchModifiers
            {
                PreferredProviders = new Provider[]
                {
                    new Provider { Code = "1G" }
                },
                
            };

            AirPricingModifiers airPricingModifiers = new AirPricingModifiers
            {
                ETicketabilitySpecified = true,
                ETicketability = typeEticketability.Required,
                FaresIndicatorSpecified = true,
                FaresIndicator = typeFaresIndicator.AllFares,
               
            };

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
                SearchPassenger = new SearchPassenger[]
                {
                    new SearchPassenger
                    {
                         Code = "ADT",
                    }
                },
                ReturnUpsellFare = true

            };

            LogService _LogService = new LogService();


            //string jRequest = JsonConvert.SerializeObject(req);
            _LogService.LogInfo($"uAPI/LowFareSearchReq", req);

            var result = client.serviceAsync(null, req).Result;

            //string jResult = JsonConvert.SerializeObject(result);
            _LogService.LogInfo($"uAPI/LowFareSearchRsp", result);
            /*




            var binding = new BasicHttpsBinding();
            binding.Name = "SystemPingPort";
            binding.CloseTimeout = TimeSpan.FromMinutes(1);
            binding.OpenTimeout = TimeSpan.FromMinutes(1);
            binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            binding.SendTimeout = TimeSpan.FromMinutes(1);
            binding.Security.Mode = BasicHttpsSecurityMode.Transport;
            binding.ProxyAddress = new Uri("http://localhost:8888");
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
                TargetBranch = "P7073862",
                BillingPointOfSaleInfo = new BillingPointOfSaleInfo
                {
                    OriginApplication = "uAPI",
                    //CIDBNumber = ""
                }
            };

            var result = client.serviceAsync(req).Result;

            Console.WriteLine(result);
            Console.ReadLine();

    */
        }
    }
}
