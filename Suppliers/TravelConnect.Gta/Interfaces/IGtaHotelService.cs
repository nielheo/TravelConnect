using System.Collections.Generic;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;

namespace TravelConnect.Gta.Interfaces
{
    public interface IGtaHotelService
    {
        Task<Hotel> GetHotel(string code, bool forceRefresh = false);
        Task<List<HotelForList>> GetHotelsForList(List<string> codes);
        Task<List<Hotel>> GetHotels(string cityCode, bool forceRefresh = false);
    }
}