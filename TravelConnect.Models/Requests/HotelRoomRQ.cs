using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Models.Requests
{
    [NotMapped]
    public class HotelRoomRQ
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int HotelId { get; set; }
        public List<RoomOccupancy> Occupancies { get; set; }
    }
}
