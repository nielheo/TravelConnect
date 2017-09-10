using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Models.Responses
{
    public class AirLowFareSearchRS
    {
        public OTA_Airlowfaresearchrs OTA_AirLowFareSearchRS { get; set; }
        public Link[] Links { get; set; }
    }

    public class OTA_Airlowfaresearchrs
    {
        public int PricedItinCount { get; set; }
        public int BrandedOneWayItinCount { get; set; }
        public int SimpleOneWayItinCount { get; set; }
        public int DepartedItinCount { get; set; }
        public int SoldOutItinCount { get; set; }
        public int AvailableItinCount { get; set; }
        public string Version { get; set; }
        public Success Success { get; set; }
        public Warnings Warnings { get; set; }
        public Priceditineraries PricedItineraries { get; set; }
    }

    public class Success
    {
    }

    public class Warnings
    {
        public Warning[] Warning { get; set; }
    }

    public class Warning
    {
        public string Type { get; set; }
        public string ShortText { get; set; }
        public string Code { get; set; }
        public string MessageClass { get; set; }
    }

    public class Priceditineraries
    {
        public Priceditinerary[] PricedItinerary { get; set; }
    }

    public class Priceditinerary
    {
        public int SequenceNumber { get; set; }
        public Airitinerary AirItinerary { get; set; }
        public Airitinerarypricinginfo[] AirItineraryPricingInfo { get; set; }
        public Ticketinginfo TicketingInfo { get; set; }
        public TPA_Extensions1 TPA_Extensions { get; set; }
    }

    public class Airitinerary
    {
        public string DirectionInd { get; set; }
        public Origindestinationoptions OriginDestinationOptions { get; set; }
    }

    public class Origindestinationoptions
    {
        public Origindestinationoption[] OriginDestinationOption { get; set; }
    }

    public class Origindestinationoption
    {
        public int ElapsedTime { get; set; }
        public Flightsegment[] FlightSegment { get; set; }
    }

    public class Flightsegment
    {
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int StopQuantity { get; set; }
        public string FlightNumber { get; set; }
        public string ResBookDesigCode { get; set; }
        public int ElapsedTime { get; set; }
        public Departureairport DepartureAirport { get; set; }
        public Arrivalairport ArrivalAirport { get; set; }
        public Operatingairline OperatingAirline { get; set; }
        public Equipment[] Equipment { get; set; }
        public Marketingairline MarketingAirline { get; set; }
        public string MarriageGrp { get; set; }
        public Departuretimezone DepartureTimeZone { get; set; }
        public Arrivaltimezone ArrivalTimeZone { get; set; }
        public TPA_Extensions TPA_Extensions { get; set; }
        public Disclosureairline DisclosureAirline { get; set; }
    }

    public class Departureairport
    {
        public string LocationCode { get; set; }
        public string TerminalID { get; set; }
    }

    public class Arrivalairport
    {
        public string LocationCode { get; set; }
        public string TerminalID { get; set; }
    }

    public class Operatingairline
    {
        public string Code { get; set; }
        public string FlightNumber { get; set; }
    }

    public class Marketingairline
    {
        public string Code { get; set; }
    }

    public class Departuretimezone
    {
        public float GMTOffset { get; set; }
    }

    public class Arrivaltimezone
    {
        public float GMTOffset { get; set; }
    }

    public class TPA_Extensions
    {
        public Eticket eTicket { get; set; }
    }

    public class Eticket
    {
        public bool Ind { get; set; }
    }

    public class Disclosureairline
    {
        public string Code { get; set; }
    }

    public class Equipment
    {
        public string AirEquipType { get; set; }
    }

    public class Ticketinginfo
    {
        public string TicketType { get; set; }
        public string ValidInterline { get; set; }
    }

    public class TPA_Extensions1
    {
        public Validatingcarrier ValidatingCarrier { get; set; }
    }

    public class Validatingcarrier
    {
        public string Code { get; set; }
    }

    public class Airitinerarypricinginfo
    {
        public string PrivateFareType { get; set; }
        public string PricingSource { get; set; }
        public string PricingSubSource { get; set; }
        public bool FareReturned { get; set; }
        public Itintotalfare ItinTotalFare { get; set; }
        public PTC_Farebreakdowns PTC_FareBreakdowns { get; set; }
        public Fareinfos1 FareInfos { get; set; }
        public TPA_Extensions6 TPA_Extensions { get; set; }
    }

    public class Itintotalfare
    {
        public Basefare BaseFare { get; set; }
        public Fareconstruction FareConstruction { get; set; }
        public Equivfare EquivFare { get; set; }
        public Taxes Taxes { get; set; }
        public Totalfare TotalFare { get; set; }
    }

    public class Basefare
    {
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class Fareconstruction
    {
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class Equivfare
    {
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class Taxes
    {
        public Tax[] Tax { get; set; }
    }

    public class Tax
    {
        public string TaxCode { get; set; }
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class Totalfare
    {
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class PTC_Farebreakdowns
    {
        public PTC_Farebreakdown[] PTC_FareBreakdown { get; set; }
    }

    public class PTC_Farebreakdown
    {
        public string PrivateFareType { get; set; }
        public Passengertypequantity PassengerTypeQuantity { get; set; }
        public Farebasiscodes FareBasisCodes { get; set; }
        public Passengerfare PassengerFare { get; set; }
        public Endorsements Endorsements { get; set; }
        public TPA_Extensions3 TPA_Extensions { get; set; }
        public Fareinfos FareInfos { get; set; }
    }

    public class Passengertypequantity
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
    }

    public class Farebasiscodes
    {
        public Farebasiscode[] FareBasisCode { get; set; }
    }

    public class Farebasiscode
    {
        public string PrivateFareType { get; set; }
        public string BookingCode { get; set; }
        public string DepartureAirportCode { get; set; }
        public string ArrivalAirportCode { get; set; }
        public string FareComponentBeginAirport { get; set; }
        public string FareComponentEndAirport { get; set; }
        public string FareComponentDirectionality { get; set; }
        public string content { get; set; }
        public bool AvailabilityBreak { get; set; }
    }

    public class Passengerfare
    {
        public Basefare1 BaseFare { get; set; }
        public Fareconstruction1 FareConstruction { get; set; }
        public Equivfare1 EquivFare { get; set; }
        public Taxes1 Taxes { get; set; }
        public Totalfare1 TotalFare { get; set; }
        public TPA_Extensions2 TPA_Extensions { get; set; }
    }

    public class Basefare1
    {
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class Fareconstruction1
    {
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class Equivfare1
    {
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class Taxes1
    {
        public Tax1[] Tax { get; set; }
        public Totaltax TotalTax { get; set; }
    }

    public class Totaltax
    {
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class Tax1
    {
        public string TaxCode { get; set; }
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class Totalfare1
    {
        public float Amount { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class TPA_Extensions2
    {
        public Surcharge[] Surcharges { get; set; }
        public Messages Messages { get; set; }
        public Baggageinformationlist BaggageInformationList { get; set; }
    }

    public class Messages
    {
        public Message[] Message { get; set; }
    }

    public class Message
    {
        public string AirlineCode { get; set; }
        public string Type { get; set; }
        public int FailCode { get; set; }
        public string Info { get; set; }
    }

    public class Baggageinformationlist
    {
        public Baggageinformation[] BaggageInformation { get; set; }
    }

    public class Baggageinformation
    {
        public string ProvisionType { get; set; }
        public string AirlineCode { get; set; }
        public Segment[] Segment { get; set; }
        public Allowance[] Allowance { get; set; }
    }

    public class Segment
    {
        public int Id { get; set; }
    }

    public class Allowance
    {
        public int Weight { get; set; }
        public string Unit { get; set; }
    }

    public class Surcharge
    {
        public string Ind { get; set; }
        public string Type { get; set; }
        public string content { get; set; }
    }

    public class Endorsements
    {
        public bool NonRefundableIndicator { get; set; }
    }

    public class TPA_Extensions3
    {
        public Farecalcline FareCalcLine { get; set; }
    }

    public class Farecalcline
    {
        public string Info { get; set; }
    }

    public class Fareinfos
    {
        public Fareinfo[] FareInfo { get; set; }
    }

    public class Fareinfo
    {
        public string FareReference { get; set; }
        public TPA_Extensions4 TPA_Extensions { get; set; }
    }

    public class TPA_Extensions4
    {
        public Seatsremaining SeatsRemaining { get; set; }
        public CabinRS Cabin { get; set; }
        public Meal Meal { get; set; }
    }

    public class Seatsremaining
    {
        public int Number { get; set; }
        public bool BelowMin { get; set; }
    }

    public class CabinRS
    {
        public string Cabin { get; set; }
    }

    public class Meal
    {
        public string Code { get; set; }
    }

    public class Fareinfos1
    {
        public Fareinfo1[] FareInfo { get; set; }
    }

    public class Fareinfo1
    {
        public string FareReference { get; set; }
        public TPA_Extensions5 TPA_Extensions { get; set; }
    }

    public class TPA_Extensions5
    {
        public Seatsremaining1 SeatsRemaining { get; set; }
        public Cabin1 Cabin { get; set; }
        public Meal1 Meal { get; set; }
    }

    public class Seatsremaining1
    {
        public int Number { get; set; }
        public bool BelowMin { get; set; }
    }

    public class Cabin1
    {
        public string Cabin { get; set; }
    }

    public class Meal1
    {
        public string Code { get; set; }
    }

    public class TPA_Extensions6
    {
        public Divideinparty DivideInParty { get; set; }
    }

    public class Divideinparty
    {
        public bool Indicator { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

}
