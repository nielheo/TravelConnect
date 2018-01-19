using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelConnect.Models.Responses
{
    [NotMapped]
    public class HotelRS
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal StarRating { get; set; }
        public string Location { get; set; }
        public string ShortDesc { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Thumbnail { get; set; }
        public string CurrCode { get; set; }
        public decimal RateFrom { get; set; }
        public decimal RateTo { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public List<RoomListRS> HotelRooms { get; set; }
        
    }

    [NotMapped]
    public class RoomListRS
    {
        public string RoomTypeCode { get; set; }
        public string RateCode { get; set; }
        public string PromoDesc { get; set; }
        public int Allotment { get; set; }
        public ChargeableRateRS ChargeableRate { get; set; }
    }

    [NotMapped]
    public class HotelSearchCityRS
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int LocationId { get; set; }
        public List<RoomOccupancy> Occupancies { get; set; }
        public string Supplier { get; set; }
        public List<HotelRS> Hotels { get; set; }
        public string Locale { get; set; }
        public string Currency { get; set; }
        public string CacheKey { get; set; }
        public string CacheLocation { get; set; }
        public string RequestKey { get; set; }

        //public static implicit operator HotelSearchCityRS(HotelSearchCityRS v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}