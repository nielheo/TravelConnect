using System.Threading.Tasks;
using TravelConnect.Models.Responses;

namespace TravelConnect.Sabre.Interfaces
{
    public interface IUtilityService
    {
        Task<AirlineRS> AirlineLookup(string code);

        Task<AirportRS> AirportLookup(string code);
    }
}