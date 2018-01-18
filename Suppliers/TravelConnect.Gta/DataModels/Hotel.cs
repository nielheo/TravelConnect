using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Gta.DataModels
{
    public class Hotel : HotelBase
    {
        


        public ICollection<HotelLocation> HotelLocations { get; set; }
        public ICollection<HotelAreaDetail> HotelAreaDetails { get; set; }
        public ICollection<HotelReport> HotelReports { get; set; }
        public ICollection<HotelRoomCategory> HotelRoomCategories { get; set; }
        public ICollection<HotelRoomType> HotelRoomTypes { get; set; }
        public ICollection<HotelFacility> HotelFacilities { get; set; }
        public ICollection<HotelRoomFacility> HotelRoomFacilities { get; set; }
        public ICollection<HotelMapLink> HotelMapLinks { get; set; }
        public ICollection<HotelImageLink> HotelImageLinks { get; set; }
    }
}
