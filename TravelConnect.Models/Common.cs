﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Models
{
    [NotMapped]
    public class FlightNumber
    {
        public string Airline { get; set; }
        public string Number { get; set; }
    }

    [NotMapped]
    public class Timing
    {
        public DateTime Time { get; set; }
        public float GmtOffset { get; set; }
    }

    [NotMapped]
    public class RoomOccupancy
    {
        public int AdultCount { get; set; }
        public List<int> ChildAges { get; set; }
    }
}
