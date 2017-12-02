using System.Threading.Tasks;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Interfaces
{
    public interface IFlightService
    {
        Task<AirlineRS> AirlineByCodeAsync(string code);
    }
}