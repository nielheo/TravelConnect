using System.Threading.Tasks;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Interfaces
{
    public interface IHotelService
    {
        Task<HotelSearchCityRS> HotelSearchByCityAsync(HotelSearchCityRQ request);
        Task<HotelSearchCityRS> HotelGetMoreAsync(HotelGetMoreRQ request);
        Task<HotelRoomRS> HotelRoomAsync(HotelRoomRQ request);
    }
}