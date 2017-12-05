using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Models.Requests
{
    [NotMapped]
    public class AirPriceSegment
    {
        public string Key { get; set; }
        public int Group { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public FlightNumber FlightNumber { get; set; }
        public Timing DepartureTime { get; set; }
        public Timing ArrivalTime { get; set; }
    }

    [NotMapped]
    public class AirPriceRQ
    {
        public List<AirPriceSegment> Segments { get; set; }
    }
}
