using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Models.Responses
{
    public class FareBreakdown
    {
    }

    public class FlightNumber
    {
        public string Airline { get; set; }
        public string Number { get; set; }
    }

    public class Timing
    {
        public DateTime Time { get; set; }
        public float GmtOffset { get; set; }
    }

    public class SegmentRS
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Timing Departure { get; set; }
        public Timing Arrival { get; set; }
        public int Elapsed { get; set; }
        public FlightNumber OperatingFlight { get; set; }
        public FlightNumber MarketingFlight { get; set; }
    }

    public class Leg
    {
        public int Elapsed { get; set; }
        public List<SegmentRS> Segments { get; set; }
    }

    public class PricedItin
    {
        public string Curr { get; set; }
        public float TotalPrice { get; set; }
        public List<FareBreakdown> FareBreakdowns { get; set; }
        public List<Leg> Legs { get; set; }
    }

    public class FlightSearchRS
    {
        public List<PricedItin> PricedItins { get; set; }
    }
}
