using kestrel.AirService;
using kestrel.SystemService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using TravelConnect.CommonServices;
using TravelConnect.uAPI.Utility;

namespace uAPI
{
    class Program
    {
        protected static string ApiKey = "sb4ps442bpwzv4fc6m9gd7rb";
        protected static string SharedSecret = "eETy7g9Y";


        protected static string GenerateSignature()
        {
            Int32 timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return CreateMD5($"{ApiKey}{SharedSecret}{timeStamp.ToString()}");
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        static void Main(string[] args)
        {

            //var binding = new BasicHttpsBinding();
            //binding.Name = "SystemPingPort";
            //binding.CloseTimeout = TimeSpan.FromMinutes(1);
            //binding.OpenTimeout = TimeSpan.FromMinutes(1);
            //binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            //binding.SendTimeout = TimeSpan.FromMinutes(1);
            //binding.Security.Mode = BasicHttpsSecurityMode.Transport;
            //binding.MaxReceivedMessageSize = Int32.MaxValue;

            ////binding.ProxyAddress = new Uri("http://localhost:8888");
            ////BasicHttpSecurityMode.Transport;

            ////            binding.Security.Transport.

            //var endpoint = new EndpointAddress("https://apac.universal-api.pp.travelport.com/B2BGateway/connect/uAPI/AirService");

            //AirLowFareSearchPortTypeClient client = new AirLowFareSearchPortTypeClient(binding, endpoint);

            ////client.ClientCredentials.UserName.UserName = "UniversalAPI/uAPI8931078193-41fe5ac8";
            ////client.ClientCredentials.UserName.Password = "kE8jAwj28td8nqQTSgtM2rhw7";

            //var httpHeaders = Helper.ReturnHttpHeader();
            //client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

            //var SearchAirLegs = new List<SearchAirLeg>();
            //SearchAirLegs.Add(new SearchAirLeg
            //{
            //    SearchOrigin = new typeSearchLocation[]
            //    {
            //        new typeSearchLocation
            //        {
            //            Item = new Airport { Code = "BKK" }
            //        }
            //    },
            //    SearchDestination = new typeSearchLocation[]
            //    {
            //        new typeSearchLocation
            //        {
            //            Item = new Airport { Code = "SIN" }
            //        }
            //    },
            //    Items = new typeFlexibleTimeSpec[]
            //    {
            //        new typeFlexibleTimeSpec
            //        {
            //            PreferredTime = DateTime.Today.AddDays(180).ToString("yyyy-MM-dd")
            //        }
            //    }
            //});

            //SearchAirLegs.Add(new SearchAirLeg
            //{
            //    SearchOrigin = new typeSearchLocation[]
            //    {
            //        new typeSearchLocation
            //        {
            //            Item = new Airport { Code = "SIN" }
            //        }
            //    },
            //    SearchDestination = new typeSearchLocation[]
            //    {
            //        new typeSearchLocation
            //        {
            //            Item = new Airport { Code = "BKK" }
            //        }
            //    },
            //    Items = new typeFlexibleTimeSpec[]
            //    {
            //        new typeFlexibleTimeSpec
            //        {
            //            PreferredTime = DateTime.Today.AddDays(185).ToString("yyyy-MM-dd")
            //        }
            //    }
            //});

            //AirSearchModifiers airSearchModifiers = new AirSearchModifiers
            //{
            //    PreferredProviders = new Provider[]
            //    {
            //        new Provider { Code = "1G" }
            //    },

            //};

            //AirPricingModifiers airPricingModifiers = new AirPricingModifiers
            //{
            //    ETicketabilitySpecified = true,
            //    ETicketability = typeEticketability.Required,
            //    FaresIndicatorSpecified = true,
            //    FaresIndicator = typeFaresIndicator.AllFares,

            //};

            //LowFareSearchReq req = new LowFareSearchReq
            //{
            //    BillingPointOfSaleInfo = new kestrel.AirService.BillingPointOfSaleInfo
            //    {
            //        OriginApplication = "uAPI"
            //    },
            //    TargetBranch = "P7073862",
            //    MaxNumberOfExpertSolutions = "50",
            //    SolutionResult = false,
            //    Items = SearchAirLegs.ToArray(),
            //    AirSearchModifiers = airSearchModifiers,
            //    AirPricingModifiers = airPricingModifiers,
            //    SearchPassenger = new SearchPassenger[]
            //    {
            //        new SearchPassenger
            //        {
            //             Code = "ADT",
            //        }
            //    },
            //    ReturnUpsellFare = true

            //};

            //LogService _LogService = new LogService();


            ////string jRequest = JsonConvert.SerializeObject(req);
            //_LogService.LogInfo($"uAPI/LowFareSearchReq", req);

            //var result = client.serviceAsync(null, req).Result;

            ////string jResult = JsonConvert.SerializeObject(result);
            //_LogService.LogInfo($"uAPI/LowFareSearchRsp", result);
            ///*




            //var binding = new BasicHttpsBinding();
            //binding.Name = "SystemPingPort";
            //binding.CloseTimeout = TimeSpan.FromMinutes(1);
            //binding.OpenTimeout = TimeSpan.FromMinutes(1);
            //binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            //binding.SendTimeout = TimeSpan.FromMinutes(1);
            //binding.Security.Mode = BasicHttpsSecurityMode.Transport;
            //binding.ProxyAddress = new Uri("http://localhost:8888");
            ////BasicHttpSecurityMode.Transport;

            ////            binding.Security.Transport.

            //var endpoint = new EndpointAddress("https://apac.universal-api.pp.travelport.com/B2BGateway/connect/uAPI/SystemService");

            //SystemPingPortTypeClient client = new SystemPingPortTypeClient(binding, endpoint);

            ////client.ClientCredentials.UserName.UserName = "UniversalAPI/uAPI8931078193-41fe5ac8";
            ////client.ClientCredentials.UserName.Password = "kE8jAwj28td8nqQTSgtM2rhw7";

            //var httpHeaders = Helper.ReturnHttpHeader();
            //client.Endpoint.EndpointBehaviors.Add(new HttpHeadersEndpointBehavior(httpHeaders));

            //PingReq req = new PingReq
            //{
            //    TraceId = "Test",
            //    Payload = "Ping test",
            //    TargetBranch = "P7073862",
            //    BillingPointOfSaleInfo = new BillingPointOfSaleInfo
            //    {
            //        OriginApplication = "uAPI",
            //        //CIDBNumber = ""
            //    }
            //};

            //var result = client.serviceAsync(req).Result;
            var result = GenerateSignature();

            Console.WriteLine(result);
            Console.ReadLine();

    
        }
    }
}
