using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Gta.DataModels
{
    [NotMapped]
    public class HotelForList : HotelBase
    {
        public HotelForList() { }

        public HotelForList(HotelBase hotelBase) : base(hotelBase) { }

        public HotelImageLink HotelImage { get; set; }
    }
}
