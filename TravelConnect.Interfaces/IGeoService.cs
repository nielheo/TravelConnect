using System.Threading.Tasks;
using TravelConnect.Models.Responses;

namespace TravelConnect.Interfaces
{
    public interface IGeoService
    {
        Task<AirportAutocompleteRS> GetAirportAutocompleteAsync(string query);

        Task<AirportRS> GetAirtportByCodeAsync(string airportCode);
    }
}