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
        private async Task<List<Country>> GetGtaCountries()
        {
            try
            {
                _LogService = new CommonServices.LogService();
                _LogService.LogInfo("GTA/SearchCountryRQ", "");

                SearchCountryRequest req = new SearchCountryRequest
                {
                    Source = Source,
                    RequestDetails = new Requestdetails
                    {
                        SearchCountryRequest = new Searchcountryrequest
                        {
                            CountryName = ""
                        }
                    }
                };

                var xRequest = Serialize(req);

                var result = await SubmitAsync(xRequest);

                var response = Deserialize<SearchCountryResponse>(result);

                List<Country> countries = new List<Country>();

                foreach (var coun in response.ResponseDetails.SearchCountryResponse.CountryDetails)
                {
                    if (countries.FirstOrDefault(c => c.Code == coun.Code) == null)
                        countries.Add(new Country
                        {
                            Code = coun.Code.ToUpper(),
                            Name = coun.Value
                        });
                }

                _LogService.LogInfo("GTA/SearchCountryRS", countries);

                return countries;
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "Gta.GeoService.GetGtaCountries");
                return new List<Country>();
            }
        }
    }
}