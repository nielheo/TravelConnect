using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;
using TravelConnect.Models;
using Microsoft.AspNetCore.Authorization;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/Hotels")]
    [RequestSizeLimit(100000000)]
    public class HotelsController : Controller
    {
        private IHotelService _HotelService;

        public HotelsController(IHotelService _HotelService)
        {
            this._HotelService = _HotelService;
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
                }).ToList()
            };

            return await _HotelService.HotelSearchByCityAsync(request);
        }
    }
}