using System;

namespace TravelConnect.Sabre.Models.PnrResponse
{
    public class CreatePassNameRecordRS
    {
        public Createpassengernamerecordrs CreatePassengerNameRecordRS { get; set; }
        public Link[] Links { get; set; }
    }

    public class Createpassengernamerecordrs
    {
        public Applicationresults ApplicationResults { get; set; }
        public Itineraryref ItineraryRef { get; set; }
        public Airbook AirBook { get; set; }
        public Travelitineraryread TravelItineraryRead { get; set; }
    }

    public class Applicationresults
    {
        public string status { get; set; }
        public Success[] Success { get; set; }
        public Warning[] Warning { get; set; }
    }

    public class Success
    {
        public DateTime timeStamp { get; set; }
    }

    public class Warning
    {
        public string type { get; set; }
        public DateTime timeStamp { get; set; }
        public Systemspecificresult[] SystemSpecificResults { get; set; }
    }

    public class Systemspecificresult
    {
        public Message[] Message { get; set; }
    }

    public class Message
    {
        public string code { get; set; }
        public string content { get; set; }
    }

    public class Itineraryref
    {
        public string ID { get; set; }
    }

    public class Airbook
    {
        public Origindestinationoption OriginDestinationOption { get; set; }
    }

    public class Origindestinationoption
    {
        public Flightsegment[] FlightSegment { get; set; }
    }

    public class Flightsegment
    {
        public string ArrivalDateTime { get; set; }
        public string DepartureDateTime { get; set; }
        public bool eTicket { get; set; }
        public string FlightNumber { get; set; }
        public string NumberInParty { get; set; }
        public string ResBookDesigCode { get; set; }
        public string Status { get; set; }
        public Destinationlocation DestinationLocation { get; set; }
        public Marketingairline MarketingAirline { get; set; }
        public Originlocation OriginLocation { get; set; }
    }

    public class Destinationlocation
    {
        public string LocationCode { get; set; }
    }

    public class Marketingairline
    {
        public string Code { get; set; }
        public string FlightNumber { get; set; }
    }

    public class Originlocation
    {
        public string LocationCode { get; set; }
    }

    public class Travelitineraryread
    {
        public Travelitinerary TravelItinerary { get; set; }
    }

    public class Travelitinerary
    {
        public Customerinfo CustomerInfo { get; set; }
        public Itineraryinfo ItineraryInfo { get; set; }
        public Itineraryref1 ItineraryRef { get; set; }
        public Specialserviceinfo[] SpecialServiceInfo { get; set; }
    }

    public class Customerinfo
    {
        public Address Address { get; set; }
        public Contactnumbers ContactNumbers { get; set; }
        public Personname[] PersonName { get; set; }
    }

    public class Address
    {
        public Addressline[] AddressLine { get; set; }
    }

    public class Addressline
    {
        public string type { get; set; }
        public string content { get; set; }
    }

    public class Contactnumbers
    {
        public Contactnumber[] ContactNumber { get; set; }
    }

    public class Contactnumber
    {
        public string LocationCode { get; set; }
        public string Phone { get; set; }
        public string RPH { get; set; }
    }

    public class Personname
    {
        public string WithInfant { get; set; }
        public string NameNumber { get; set; }
        public string NameReference { get; set; }
        public string RPH { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
    }

    public class Itineraryinfo
    {
        public Reservationitems ReservationItems { get; set; }
        public Ticketing[] Ticketing { get; set; }
    }

    public class Reservationitems
    {
        public Item[] Item { get; set; }
    }

    public class Item
    {
        public string RPH { get; set; }
        public Flightsegment1[] FlightSegment { get; set; }
    }

    public class Flightsegment1
    {
        public string AirMilesFlown { get; set; }
        public string ArrivalDateTime { get; set; }
        public string DayOfWeekInd { get; set; }
        public string DepartureDateTime { get; set; }
        public string ElapsedTime { get; set; }
        public bool eTicket { get; set; }
        public string FlightNumber { get; set; }
        public string NumberInParty { get; set; }
        public string ResBookDesigCode { get; set; }
        public string SegmentNumber { get; set; }
        public bool SmokingAllowed { get; set; }
        public bool SpecialMeal { get; set; }
        public string Status { get; set; }
        public string StopQuantity { get; set; }
        public bool IsPast { get; set; }
        public Destinationlocation1 DestinationLocation { get; set; }
        public Equipment Equipment { get; set; }
        public Marketingairline1 MarketingAirline { get; set; }
        public Meal[] Meal { get; set; }
        public Originlocation1 OriginLocation { get; set; }
        public Supplierref SupplierRef { get; set; }
        public string UpdatedArrivalTime { get; set; }
        public string UpdatedDepartureTime { get; set; }
    }

    public class Destinationlocation1
    {
        public string LocationCode { get; set; }
    }

    public class Equipment
    {
        public string AirEquipType { get; set; }
    }

    public class Marketingairline1
    {
        public string Code { get; set; }
        public string FlightNumber { get; set; }
    }

    public class Originlocation1
    {
        public string LocationCode { get; set; }
    }

    public class Supplierref
    {
        public string ID { get; set; }
    }

    public class Meal
    {
        public string Code { get; set; }
    }

    public class Ticketing
    {
        public string RPH { get; set; }
        public string TicketTimeLimit { get; set; }
    }

    public class Itineraryref1
    {
        public bool AirExtras { get; set; }
        public string ID { get; set; }
        public string InhibitCode { get; set; }
        public string PartitionID { get; set; }
        public string PrimeHostID { get; set; }
        public Source Source { get; set; }
    }

    public class Source
    {
        public string AAA_PseudoCityCode { get; set; }
        public string CreateDateTime { get; set; }
        public string CreationAgent { get; set; }
        public string HomePseudoCityCode { get; set; }
        public string PseudoCityCode { get; set; }
        public string ReceivedFrom { get; set; }
        public string LastUpdateDateTime { get; set; }
        public string SequenceNumber { get; set; }
    }

    public class Specialserviceinfo
    {
        public string RPH { get; set; }
        public string Type { get; set; }
        public Service Service { get; set; }
    }

    public class Service
    {
        public string SSR_Code { get; set; }
        public string SSR_Type { get; set; }
        public Airline Airline { get; set; }
        public string[] Text { get; set; }
    }

    public class Airline
    {
        public string Code { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }
}