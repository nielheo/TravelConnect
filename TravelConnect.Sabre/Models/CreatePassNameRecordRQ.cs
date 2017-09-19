using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Sabre.Models.Pnr
{
    public class CreatePassNameRecordRQ
    {
        public Createpassengernamerecordrq CreatePassengerNameRecordRQ { get; set; }
    }

    public class Createpassengernamerecordrq
    {
        public string targetCity { get; set; }
        public Profile Profile { get; set; }
        public Airbook AirBook { get; set; }
        public Airprice AirPrice { get; set; }
        public Miscsegment MiscSegment { get; set; }
        public Specialreqdetails SpecialReqDetails { get; set; }
        public Postprocessing PostProcessing { get; set; }
    }

    public class Profile
    {
        public Uniqueid UniqueID { get; set; }
    }

    public class Uniqueid
    {
        public string ID { get; set; }
    }

    public class Airbook
    {
        public Origindestinationinformation OriginDestinationInformation { get; set; }
    }

    public class Origindestinationinformation
    {
        public Flightsegment[] FlightSegment { get; set; }
    }

    public class Flightsegment
    {
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public int FlightNumber { get; set; }
        public int NumberInParty { get; set; }
        public string ResBookDesigCode { get; set; }
        public string Status { get; set; }
        public Destinationlocation DestinationLocation { get; set; }
        public Marketingairline MarketingAirline { get; set; }
        public string MarriageGrp { get; set; }
        public Originlocation OriginLocation { get; set; }
    }

    public class Destinationlocation
    {
        public string LocationCode { get; set; }
    }

    public class Marketingairline
    {
        public string Code { get; set; }
        public int FlightNumber { get; set; }
    }

    public class Originlocation
    {
        public string LocationCode { get; set; }
    }

    public class Airprice
    {
        public Pricerequestinformation PriceRequestInformation { get; set; }
    }

    public class Pricerequestinformation
    {
        public Optionalqualifiers OptionalQualifiers { get; set; }
    }

    public class Optionalqualifiers
    {
        public Miscqualifiers MiscQualifiers { get; set; }
        public Pricingqualifiers PricingQualifiers { get; set; }
    }

    public class Miscqualifiers
    {
        public Tourcode TourCode { get; set; }
    }

    public class Tourcode
    {
        public string Text { get; set; }
    }

    public class Pricingqualifiers
    {
        public Passengertype[] PassengerType { get; set; }
    }

    public class Passengertype
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
    }

    public class Miscsegment
    {
        public string DepartureDateTime { get; set; }
        public int NumberInParty { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public Originlocation1 OriginLocation { get; set; }
        public string Text { get; set; }
        public Vendorprefs VendorPrefs { get; set; }
    }

    public class Originlocation1
    {
        public string LocationCode { get; set; }
    }

    public class Vendorprefs
    {
        public Airline Airline { get; set; }
    }

    public class Airline
    {
        public string Code { get; set; }
    }

    public class Specialreqdetails
    {
        public Addremark AddRemark { get; set; }
        public Airseat AirSeat { get; set; }
        public Specialservice SpecialService { get; set; }
    }

    public class Addremark
    {
        public Remarkinfo RemarkInfo { get; set; }
    }

    public class Remarkinfo
    {
        public FOP_Remark FOP_Remark { get; set; }
        public Futurequeueplaceremark FutureQueuePlaceRemark { get; set; }
        public Remark[] Remark { get; set; }
    }

    public class FOP_Remark
    {
        public string Type { get; set; }
        public CC_Info CC_Info { get; set; }
    }

    public class CC_Info
    {
        public bool Suppress { get; set; }
        public Paymentcard PaymentCard { get; set; }
    }

    public class Paymentcard
    {
        public string AirlineCode { get; set; }
        public string CardSecurityCode { get; set; }
        public string Code { get; set; }
        public string ExpireDate { get; set; }
        public string ExtendedPayment { get; set; }
        public string ManualApprovalCode { get; set; }
        public string Number { get; set; }
        public bool SuppressApprovalCode { get; set; }
    }

    public class Futurequeueplaceremark
    {
        public string Date { get; set; }
        public string PrefatoryInstructionCode { get; set; }
        public string PseudoCityCode { get; set; }
        public string QueueIdentifier { get; set; }
        public string Time { get; set; }
    }

    public class Remark
    {
        public string Type { get; set; }
        public string Text { get; set; }
    }

    public class Airseat
    {
        public Seats Seats { get; set; }
    }

    public class Seats
    {
        public Seat[] Seat { get; set; }
    }

    public class Seat
    {
        public string NameNumber { get; set; }
        public string Preference { get; set; }
        public string SegmentNumber { get; set; }
    }

    public class Specialservice
    {
        public Specialserviceinfo SpecialServiceInfo { get; set; }
    }

    public class Specialserviceinfo
    {
        public Service[] Service { get; set; }
    }

    public class Service
    {
        public string SSR_Code { get; set; }
        public Personname PersonName { get; set; }
        public string Text { get; set; }
        public Vendorprefs1 VendorPrefs { get; set; }
    }

    public class Personname
    {
        public string NameNumber { get; set; }
    }

    public class Vendorprefs1
    {
        public Airline1 Airline { get; set; }
    }

    public class Airline1
    {
        public string Code { get; set; }
    }

    public class Postprocessing
    {
        public bool RedisplayReservation { get; set; }
        public string ARUNK { get; set; }
        public Queueplace QueuePlace { get; set; }
        public Endtransaction EndTransaction { get; set; }
    }

    public class Queueplace
    {
        public Queueinfo QueueInfo { get; set; }
    }

    public class Queueinfo
    {
        public Queueidentifier[] QueueIdentifier { get; set; }
    }

    public class Queueidentifier
    {
        public string Number { get; set; }
        public string PrefatoryInstructionCode { get; set; }
    }

    public class Endtransaction
    {
        public Source Source { get; set; }
    }

    public class Source
    {
        public string ReceivedFrom { get; set; }
    }

}
