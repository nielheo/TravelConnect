using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Models.Requests
{
    public class AirLowFareSearchRQ
    {
        public OTA_Airlowfaresearchrq OTA_AirLowFareSearchRQ { get; set; }
    }

    public class OTA_Airlowfaresearchrq
    {
        public bool DirectFlightsOnly { get; set; }
        public bool AvailableFlightsOnly { get; set; }
        public string Version { get; set; }
        public string Target { get; set; }
        public POS POS { get; set; }
        public Origindestinationinformation[] OriginDestinationInformation { get; set; }
        public Travelpreferences TravelPreferences { get; set; }
        public Travelerinfosummary TravelerInfoSummary { get; set; }
        public TPA_Extensions2 TPA_Extensions { get; set; }
    }

    public class POS
    {
        public Source[] Source { get; set; }
    }

    public class Source
    {
        public string PseudoCityCode { get; set; }
        public Requestorid RequestorID { get; set; }
    }

    public class Requestorid
    {
        public string Type { get; set; }
        public string ID { get; set; }
        public Companyname CompanyName { get; set; }
    }

    public class Companyname
    {
        public string Code { get; set; }
        public string content { get; set; }
    }

    public class Travelpreferences
    {
        public bool ETicketDesired { get; set; }
        public bool ValidInterlineTicket { get; set; }
        public bool SmokingAllowed { get; set; }
        public Cabinpref[] CabinPref { get; set; }
        public Vendorpref[] VendorPref { get; set; }

        public TPA_Extensions TPA_Extensions { get; set; }
    }

    public class TPA_Extensions
    {
        public Onlineindicator OnlineIndicator { get; set; }
        public Triptype TripType { get; set; }
    }

    public class Onlineindicator
    {
        public bool Ind { get; set; }
    }

    public class Triptype
    {
        public string Value { get; set; }
    }

    public class Cabinpref
    {
        public string Cabin { get; set; }
        public string PreferLevel { get; set; }
    }

    public class Vendorpref
    {
        public string Code { get; set; }

        //Valid values are: 'Only', 'Unacceptable', 'Preferred'.
        public string PreferLevel { get; set; }
    }

    public class Travelerinfosummary
    {
        public bool SpecificPTC_Indicator { get; set; }
        public int[] SeatsRequested { get; set; }
        public Airtraveleravail[] AirTravelerAvail { get; set; }
        public Pricerequestinformation PriceRequestInformation { get; set; }
    }

    public class Pricerequestinformation
    {
        public bool NegotiatedFaresOnly { get; set; }
        public bool Reprice { get; set; }
        public bool ProcessThruFaresOnly { get; set; }
        public string CurrencyCode { get; set; }
        public TPA_Extensions1 TPA_Extensions { get; set; }
    }

    public class TPA_Extensions1
    {
        public Priority Priority { get; set; }
    }

    public class Priority
    {
        public Price Price { get; set; }
        public Directflights DirectFlights { get; set; }
        public Time Time { get; set; }
        public Vendor Vendor { get; set; }
    }

    public class Price
    {
        public int Priority { get; set; }
    }

    public class Directflights
    {
        public int Priority { get; set; }
    }

    public class Time
    {
        public int Priority { get; set; }
    }

    public class Vendor
    {
        public int Priority { get; set; }
    }

    public class Airtraveleravail
    {
        public Passengertypequantity[] PassengerTypeQuantity { get; set; }
    }

    public class Passengertypequantity
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
        public bool Changeable { get; set; }
    }

    public class TPA_Extensions2
    {
        public Intelliselltransaction IntelliSellTransaction { get; set; }
    }

    public class Intelliselltransaction
    {
        public Requesttype RequestType { get; set; }
    }

    public class Requesttype
    {
        public string Name { get; set; }
    }

    public class Origindestinationinformation
    {
        public string RPH { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public Originlocation OriginLocation { get; set; }
        public Destinationlocation DestinationLocation { get; set; }
    }

    public class Originlocation
    {
        public string LocationCode { get; set; }
    }

    public class Destinationlocation
    {
        public string LocationCode { get; set; }
    }

}
