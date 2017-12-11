using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}