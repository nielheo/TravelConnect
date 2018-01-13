using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TravelConnect.Gta.Models
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("Request", Namespace = "", IsNullable = false)]
    public class SearchHotelPriceRequest : BaseRequest
    {

    }
    
    public partial class Requestdetails 
    {
        public Searchhotelpricerequest SearchHotelPriceRequest { get; set; }
    }

    public class Searchhotelpricerequest
    {
        public Itemdestination ItemDestination { get; set; }
        public string ImmediateConfirmationOnly { get; set; }
        public Periodofstay PeriodOfStay { get; set; }
        public string IncludeRecommended { get; set; }
        public string IncludePriceBreakdown { get; set; }
        public string IncludeChargeConditions { get; set; }
        public Excludechargeableitems ExcludeChargeableItems { get; set; }
        public Room[] Rooms { get; set; }
        public Starrating StarRating { get; set; }
        public string LocationCode { get; set; }
        public Facilitycodes FacilityCodes { get; set; }
        public string OrderBy { get; set; }
        public string NumberOfReturnedItems { get; set; }
    }

    public class Itemdestination
    {
        [XmlAttribute]
        public string DestinationType { get; set; }

        [XmlAttribute]
        public string DestinationCode { get; set; }
        public string WestLongitude { get; set; }
        public string SouthLatitude { get; set; }
        public string EastLongitude { get; set; }
        public string NorthLatitude { get; set; }
    }

    public class Periodofstay
    {
        public string CheckInDate { get; set; }
        public int Duration { get; set; }
    }

    public class Excludechargeableitems
    {
        public string CancellationDeadlineDays { get; set; }
    }
    
    public class Room
    {
        public Extrabeds ExtraBeds { get; set; }

        [XmlAttribute]
        public string Code { get; set; }
        public string NumberOfRooms { get; set; }
        public string NumberOfCots { get; set; }
    }

    public class Extrabeds
    {
        public string Age { get; set; }
    }

    public class Starrating
    {
        public string _MinimumRating { get; set; }
        public string __text { get; set; }
    }

    public class Facilitycodes
    {
        public string[] FacilityCode { get; set; }
    }
}
