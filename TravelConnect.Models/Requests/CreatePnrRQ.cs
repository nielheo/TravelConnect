using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Models.CreatePnr
{
    public class CreatePnrRQ
    {
        public List<AirSegment> AirSegments { get; set; }
        public List<Passenger> Passengers { get; set; }
    }

    public class AirSegment
    {
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Airline MarketingAirline { get; set; }
        public int NumberInParty { get; set; }
        public string Brd { get; set; }
        public string Status { get; set; }
        public string MarriageGrp { get; set; }
    }

    public class Airline
    {
        public string Code { get; set; }
        public int Number { get; set; }
    }

    public class Passenger
    {
        public string Type { get; set; }
    }
}
