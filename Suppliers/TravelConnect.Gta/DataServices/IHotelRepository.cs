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
        
        void InsertHotel(Hotel hotel);

        void InsertHotels(List<Hotel> hotels, string cityCode);
    }
}
