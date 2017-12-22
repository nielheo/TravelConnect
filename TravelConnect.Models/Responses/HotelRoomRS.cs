using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Models.Responses
{
    [NotMapped]
    public class HotelRoomRS
    {
        public int HotelId { get; set; }
    }
}
