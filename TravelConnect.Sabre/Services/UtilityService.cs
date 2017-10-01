using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre.Interfaces;

namespace TravelConnect.Sabre.Services
{
    public class UtilityService : IUtilityService
    {
        private ISabreConnector _SabreConnector;

        public UtilityService(ISabreConnector _SabreConnector)
        {
            this._SabreConnector = _SabreConnector;
        }

        public async Task<AirlineRS> AirlineLookup(string code)
        {
            try
            {
                var result = await _SabreConnector.SendRequestAsync("/v1/lists/utilities/airlines",
                    "airlinecode=" + code, false);


                Models.AirlineRS rs = JsonConvert.DeserializeObject<Models.AirlineRS>(result);

                if (rs.AirlineInfo?.FirstOrDefault() == null)
                    return null;

                return new AirlineRS
                {
                    Code = rs.AirlineInfo.FirstOrDefault().AirlineCode,
                    Name = rs.AirlineInfo.FirstOrDefault().AirlineName
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
