using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;

namespace TravelConnect.Gta.DataServices
{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotel(string code);

        Task<Hotel> GetHotelWithDetails(string code);

        Task<List<Hotel>> GetHotels(string cityCode);

        Task<List<Hotel>> GetHotelsWithDetails(string cityCode);

        Task<Hotel> GetHotelWithImages(string code);
        
        Task InsertHotel(Hotel hotel);

        Task InsertHotels(List<Hotel> hotels, string cityCode);
    }
}
