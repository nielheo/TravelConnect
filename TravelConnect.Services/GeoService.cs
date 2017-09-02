using Newtonsoft.Json;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Responses;
using TravelConnect.Sabre;
using TravelConnect.Sabre.Models;

namespace TravelConnect.Services
{
    public class GeoService : IGeoService
    {
        ISabreConnector _SabreConnector;

        public GeoService(ISabreConnector _SabreConnector)
        {
            this._SabreConnector = _SabreConnector;
        }

        public async Task<AirportAutocompleteRS> GetAirportAutocompleteAsync(string query)
        {
            var result = await _SabreConnector.SendRequestAsync("/v1/lists/utilities/geoservices/autocomplete",
                "query=" + query + "&category=AIR&limit=30", false);

            result = result.Replace("category:AIR", "categoryAIR");

            AutocompleteRS rs = JsonConvert.DeserializeObject<AutocompleteRS>(result);

            AirportAutocompleteRS autocompleteRs = new AirportAutocompleteRS()
            {
                AirportsRS = new System.Collections.Generic.List<AirportRS>()
            };

            foreach (var doc in rs.Response.grouped.categoryAIR.doclist.docs)
            {
                autocompleteRs.AirportsRS.Add(new AirportRS
                {
                    Code = doc.id,
                    Name = doc.name,
                    IataCityCode = doc.iataCityCode,
                    CityName = doc.city,
                    CountryCode = doc.country,
                    CountryName = doc.countryName,
                });
            }

            return autocompleteRs;
        }
    }
}
