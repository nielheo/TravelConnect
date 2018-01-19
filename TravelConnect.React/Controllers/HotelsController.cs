using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.CommonServices;
using TravelConnect.Gta.DataModels;
using TravelConnect.Gta.Interfaces;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/Hotels")]
    [RequestSizeLimit(100000000)]
    public class HotelsController : Controller
    {
        private IHotelService _HotelService;
        private IGtaHotelService _GtaHotelService;

        protected LogService _LogService = null;

        public HotelsController(IHotelService _HotelService,
            IGtaHotelService _GtaHotelService)
        {
            this._HotelService = _HotelService;
            this._GtaHotelService = _GtaHotelService;
        }

        [Route("more")]
        public async Task<HotelSearchCityRS> Get(string locale, string currency, string cacheKey,
            string cacheLocation, string requestKey)
        {
            HotelGetMoreRQ request = new HotelGetMoreRQ
            {
                Locale = locale,
                Currency = currency,
                CacheKey = cacheKey,
                CacheLocation = cacheLocation,
                RequestKey = requestKey,
                Suppliers = new List<string> { "EAN" }
            };

            return await _HotelService.HotelGetMoreAsync(request);
        }

        [Route("{country}/{city}")]
        public async Task<HotelSearchCityRS> Get(string country, string city, DateTime checkIn, DateTime checkOut, string locale, string currency, string rooms)
        {
            HotelSearchCityRQ request = new HotelSearchCityRQ
            {
                CheckIn = checkIn,
                CheckOut = checkOut,
                City = city,
                Country = country,
                Locale = locale,
                Currency = currency,
                Suppliers = new List<string> { "EAN", "GTA" },
                Occupancies = rooms.Split('|').ToList().Select(r =>
                {
                    var occu = new RoomOccupancy
                    {
                        AdultCount = Convert.ToInt32(r.Split(',')[0])
                    };

                    if (r.Split(',').Length > 1)
                    {
                        occu.ChildAges = r.Split(',').Skip(1).Select(c => Convert.ToInt32(c)).ToList();
                    }

                    return occu;
                }).ToList()
            };

            return await _HotelService.HotelSearchByCityAsync(request);
        }

        [Route("{country}/{city}/{id}/rooms")]
        public async Task<HotelRoomRS> GetRoom(string country, string city, int id, DateTime checkIn, DateTime checkOut, string locale, string currency, string rooms)
        {
            HotelRoomRQ request = new HotelRoomRQ()
            {
                CheckIn = checkIn,
                CheckOut = checkOut,
                Currency = currency,
                Locale = locale,
                HotelId = id,
                Suppliers = new List<string> { "EAN" },
                Occupancies = rooms.Split('|').ToList().Select(r =>
                {
                    var occu = new RoomOccupancy
                    {
                        AdultCount = Convert.ToInt32(r.Split(',')[0])
                    };

                    if (r.Split(',').Length > 1)
                    {
                        occu.ChildAges = r.Split(',').Skip(1).Select(c => Convert.ToInt32(c)).ToList();
                    }

                    return occu;
                }).ToList(),
            };

            return await _HotelService.HotelRoomAsync(request);
        }

        [Route("{country}/{city}/{id}/detail")]
        public async Task<Hotel> GetDetail(string country, string city, string id)
        {
            var hotel = await _GtaHotelService.GetHotel($"{city}.{id}");

            return hotel;
        }

        [Route("{ids}/info")]
        public async Task<List<HotelForList>> GetInfo(string ids)
        {
            List<string> codes = ids.Split(',').ToList();

            var hotel = await _GtaHotelService.GetHotelsForList(codes);

            return hotel;
        }


        [Route("{id}/recheck")]
        public async Task<HotelRoomRS> RecheckPrice(int id, DateTime checkIn, DateTime checkOut,
            string locale, string currency, string rooms, string rateCode, string roomTypeCode)
        {
            HotelRecheckPriceRQ request = new HotelRecheckPriceRQ()
            {
                CheckIn = checkIn,
                CheckOut = checkOut,
                Currency = currency,
                Locale = locale,
                HotelId = id,
                Suppliers = new List<string> { "EAN" },
                RateCode = rateCode,
                RoomTypeCode = roomTypeCode,
                Occupancies = rooms.Split('|').ToList().Select(r =>
                {
                    var occu = new RoomOccupancy
                    {
                        AdultCount = Convert.ToInt32(r.Split(',')[0])
                    };

                    if (r.Split(',').Length > 1)
                    {
                        occu.ChildAges = r.Split(',').Skip(1).Select(c => Convert.ToInt32(c)).ToList();
                    }

                    return occu;
                }).ToList(),
            };

            return await _HotelService.HotelRecheckPriceAsync(request);
        }
    }
}