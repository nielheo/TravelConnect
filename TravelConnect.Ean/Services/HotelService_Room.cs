using System;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Ean.Services
{
    public partial class HotelService : BaseService, IHotelService
    {
        public async Task<HotelRoomRS> HotelRoomAsync(HotelRoomRQ request)
        {
            throw new NotImplementedException();
        }
    }
}