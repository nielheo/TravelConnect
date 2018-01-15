using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;
using TravelConnect.Gta.Interfaces;

namespace TravelConnect.Gta.Services
{
    public class GtaHotelService : IGtaHotelService
    {
        public Task<Hotel> GetHotel(string code, bool forceRefresh)
        {
            throw new NotImplementedException();
        }

        public Task<List<Hotel>> GetHotels(string cityCode, bool forceRefresh)
        {
            throw new NotImplementedException();
        }
    }
}
