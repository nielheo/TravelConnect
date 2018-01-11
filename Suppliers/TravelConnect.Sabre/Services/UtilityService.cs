using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.CommonServices;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre.Interfaces;
using TravelConnect.Sabre.Models;

namespace TravelConnect.Sabre.Services
{
    public class UtilityService : IUtilityService
    {
        private ISabreConnector _SabreConnector;
        private ILogService _LogService;

        public UtilityService(ISabreConnector _SabreConnector,
            ILogService _LogService)
        {
            this._SabreConnector = _SabreConnector;
            this._LogService = _LogService;
        }

        public async Task<TravelConnect.Models.Responses.AirlineRS> AirlineLookup(string code)
        {
            try
            {
                var result = await _SabreConnector.SendRequestAsync("/v1/lists/utilities/airlines",
                    "airlinecode=" + code, false);

                Models.AirlineRS rs = JsonConvert.DeserializeObject<Models.AirlineRS>(result);

                if (rs.AirlineInfo?.FirstOrDefault() == null)
                    return null;

                return new TravelConnect.Models.Responses.AirlineRS
                {
                    Code = rs.AirlineInfo.FirstOrDefault().AirlineCode,
                    Name = rs.AirlineInfo.FirstOrDefault().AirlineName
                };
            }
            catch (Exception ex)
            {
                var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
                var fullMethodName = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;

                _LogService.LogException(ex, fullMethodName);

                return null;
            }
        }

        public async Task<AirportRS> AirportLookup(string code)
        {
            string result = await _SabreConnector.SendRequestAsync("/v1/lists/utilities/geocode/locations",
                JsonConvert.SerializeObject(ConvertToGeoCodeRQ(code).Request,
                    Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatString = "yyyy-MM-ddTHH:mm:ss"
                    }), true);

            GeoCodeRS rs = JsonConvert.DeserializeObject<GeoCodeRS>(result);

            return ConvertToAirportRS(rs);
        }

        private GeoCodeRQ ConvertToGeoCodeRQ(string airportCode)
        {
            GeoCodeRQ rq = new GeoCodeRQ
            {
                Request = new Class1[]
                {
                    new Class1
                    {
                        GeoCodeRQ = new Geocoderq {
                            PlaceById = new Placebyid
                            {
                                Id = airportCode,
                                BrowseCategory = new Browsecategory
                                {
                                        name = "AIR"
                                }
                            }
                        }
                    }
                }
            };

            return rq;
        }

        private AirportRS ConvertToAirportRS(GeoCodeRS response)
        {
            var place = response.Results?.FirstOrDefault()?.GeoCodeRS?.Place?.FirstOrDefault();

            if (place == null)
                return null;

            AirportRS rs = new AirportRS
            {
                Code = place.Id,
                Name = place.Name,
                Longitude = (decimal)place.longitude,
                Latitude = (decimal)place.latitude,
                CityName = place.City,
                CountryCode = place.Country
            };

            return rs;
        }
    }
}