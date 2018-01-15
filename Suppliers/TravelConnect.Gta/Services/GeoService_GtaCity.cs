using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;
using TravelConnect.Gta.Interfaces;
using TravelConnect.Gta.Models;

namespace TravelConnect.Gta.Services
{
    public partial class GeoService : BaseService, IGeoService
    {
        private async Task<List<City>> GetGtaCities(string countryCode)
        {
            countryCode = countryCode.ToUpper();

            try
            {
                _LogService = new CommonServices.LogService();
                _LogService.LogInfo("GTA/SearchCityRQ", countryCode);

                SearchCountryRequest req = new SearchCountryRequest
                {
                    Source = Source,
                    RequestDetails = new Requestdetails
                    {
                        SearchCityRequest = new Searchcityrequest
                        {
                            CountryCode = countryCode
                        }
                    }
                };

                var xRequest = Serialize(req);

                var result = await SubmitAsync(xRequest);

                var response = Deserialize<SearchCityResponse>(result);

                List<City> cities = new List<City>();

                foreach (var city in response.ResponseDetails.SearchCityResponse.CityDetails)
                {
                    if (cities.FirstOrDefault(c => c.Code == city.Code) == null)
                        cities.Add(new City
                        {
                            Code = city.Code.ToUpper(),
                            Name = city.Value,
                            CountryCode = countryCode
                        });
                }

                _LogService.LogInfo("GTA/SearchCityRS", cities);

                return cities;
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "Gta.GeoService.GetGtaCities");
                return new List<City>();
            }
        }
    }
}