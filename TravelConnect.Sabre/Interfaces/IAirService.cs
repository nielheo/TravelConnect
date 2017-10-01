using System.Collections.Generic;
using System.Threading.Tasks;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Sabre.Interfaces
{
    public interface IAirService
    {
        Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request);
        Task<List<string>> GetTopDestinationsAsync(string airportCode);
    }
}