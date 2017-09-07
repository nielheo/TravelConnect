using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TravelConnect.Interfaces;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre;
using TravelConnect.Services.Models;

namespace TravelConnect.Services
{
    public class FlightService : IFlightService
    {
        ISabreConnector _SabreConnector;

        public FlightService(ISabreConnector _SabreConnector)
        {
            this._SabreConnector = _SabreConnector;
        }

        public AirLowFareSearchRS AirLowFareSearch(SearchRQ request)
        {
            string result = 
            _SabreConnector.SendRequestAsync("/v3.2.0/shop/flights?mode=live&limit=200&offset=1",
                JsonConvert.SerializeObject(request.AirLowFareSearchRQ,
                    Formatting.None, new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            DateFormatString = "yyyy-MM-ddTHH:mm:ss"
                    }), true).Result;

            AirLowFareSearchRS rs = 
            JsonConvert.DeserializeObject<AirLowFareSearchRS>(result);

            return rs;
        }
    }
}
