using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Models.Responses
{
    [NotMapped]
    public class FareBreakdown
    {
    }

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
    public class SegmentRS
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Timing Departure { get; set; }
        public Timing Arrival { get; set; }
        public int Elapsed { get; set; }
        public FlightNumber OperatingFlight { get; set; }
        public FlightNumber MarketingFlight { get; set; }
        public string BRD { get; set; }
    }

    [NotMapped]
    public class Leg
    {
        public int Elapsed { get; set; }
        public List<SegmentRS> Segments { get; set; }
    }

    [NotMapped]
    public class Fare
    {
        public string Curr { get; set; }
        public float Amount { get; set; }
    }

    [NotMapped]
    public class PricedItin
    {
        public Fare BaseFare { get; set; }
        public Fare Taxes { get; set; }
        public Fare TotalFare { get; set; }

        public List<FareBreakdown> FareBreakdowns { get; set; }
        public List<Leg> Legs { get; set; }
        public DateTime? LastTicketDate { get; set; }
    }

    [NotMapped]
    public class FlightSearchRS
    {
        public List<PricedItin> PricedItins { get; set; }
        public string RequestId { get; set; }
        public Page Page { get; set; }
        public List<string> Airlines { get; set; }
    }

    [NotMapped]
    public class Page
    {
        public int Size { get; set; }
        public int TotalTags { get; set; }
        public int Offset { get; set; }
    }
}