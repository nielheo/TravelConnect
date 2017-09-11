using System.Threading.Tasks;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Interfaces
{
    public interface IFlightService
    {
        Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request);
        Task<FlightSearchRS> NextAirLowFareSearchAsync(string requestId, int page);

        Task<AirlineRS> GetAirlineAsync(string code);
    }
}