using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Gta.Services
{
    public partial class HotelService : BaseService, IHotelService
    {
        public Task<HotelSearchCityRS> HotelGetMoreAsync(HotelGetMoreRQ request)
        {
            throw new NotImplementedException();
        }

        public Task<HotelRoomRS> HotelRecheckPriceAsync(HotelRecheckPriceRQ request)
        {
            throw new NotImplementedException();
        }

        public Task<HotelRoomRS> HotelRoomAsync(HotelRoomRQ request)
        {
            throw new NotImplementedException();
        }
    }
}
