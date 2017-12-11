using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Models.Requests
{
    [NotMapped]
    public class HotelSearchCityRQ
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int LocationId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<RoomOccupancy> Occupancies { get; set; }
        public List<string> Suppliers { get; set; }
    }
}