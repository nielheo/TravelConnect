using System.Threading.Tasks;
using TravelConnect.Interfaces.Models;

namespace TravelConnect.Interfaces
{
    public interface IFlightService
    {
        Task<SearchRS> AirLowFareSearch(SearchRQ request);
    }
}