using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Models.Responses
{
    [NotMapped]
    public class FareInfo
    {
        public string Ptc { get; set; }
        public List<FareInfoDetail> FareInfoDetails { get; set; }
    }

    [NotMapped]
    public class FareInfoDetail
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string FareBasis { get; set; }
        public bool IsPrivateFare { get; set; }
        public Fare Amount { get; set; }
        public Brand Brand { get; set; }
    }

    [NotMapped]
    public class Brand
    {
        public string Key { get; set; }
        public string BrandId { get; set; }
        public bool UpsellBrandFound { get; set; }
        public string Name { get; set; }
        public string Carrier { get; set; }
    }
    
    [NotMapped]
    public class SegmentRS
    {
        public string Key { get; set; }
        public int Group { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Timing Departure { get; set; }
        public Timing Arrival { get; set; }
        public int Elapsed { get; set; }
        public FlightNumber OperatingFlight { get; set; }
        public FlightNumber MarketingFlight { get; set; }
        public string BookingCode { get; set; }
        public string CabinClass { get; set; }
        public string MarriageGrp { get; set; }
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

        public List<FareInfo> FareInfos { get; set; }
        public List<Leg> Legs { get; set; }
        public DateTime? LastTicketDate { get; set; }
    }

    [NotMapped]
    public class FlightSearchRS
    {
        public List<PricedItin> PricedItins { get; set; }
        public List<PricedItin> DepartPricedItins { get; set; }
        public List<PricedItin> ReturnPricedItins { get; set; }

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