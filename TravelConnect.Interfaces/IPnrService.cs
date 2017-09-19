using System.Threading.Tasks;
using TravelConnect.Models.CreatePnr;
using TravelConnect.Models.Responses;

namespace TravelConnect.Interfaces
{
    public interface IPnrService
    {
        Task<CreatePnrRS> CreatePnrAsync(CreatePnrRQ request);
    }
}