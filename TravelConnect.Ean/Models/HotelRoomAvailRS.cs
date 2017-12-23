using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Ean.Models.Rooms
{
    public class HotelRoomAvailRS
    {
        public Hotelroomavailabilityresponse HotelRoomAvailabilityResponse { get; set; }
    }

    public class Hotelroomavailabilityresponse
    {
        public string size { get; set; }
        public string customerSessionId { get; set; }
        public int hotelId { get; set; }
        public string arrivalDate { get; set; }
        public string departureDate { get; set; }
        public string hotelName { get; set; }
        public string hotelAddress { get; set; }
        public string hotelCity { get; set; }
        public string hotelCountry { get; set; }
        public int numberOfRoomsRequested { get; set; }
        public string checkInInstructions { get; set; }
        public Hotelroomresponse[] HotelRoomResponse { get; set; }
    }

    public class Hotelroomresponse
    {
        public int rateCode { get; set; }
        public int roomTypeCode { get; set; }
        public string rateDescription { get; set; }
        public string roomTypeDescription { get; set; }
        public string supplierType { get; set; }
        public int propertyId { get; set; }
        public Bedtypes BedTypes { get; set; }
        public string smokingPreferences { get; set; }
        public int rateOccupancyPerRoom { get; set; }
        public int quotedOccupancy { get; set; }
        public int minGuestAge { get; set; }
        public Rateinfos RateInfos { get; set; }
        public Valueadds ValueAdds { get; set; }
        public string deepLink { get; set; }
    }

    public class Bedtypes
    {
        public string size { get; set; }
        public object BedType { get; set; }
    }

    public class Rateinfos
    {
        public string size { get; set; }
        public Rateinfo RateInfo { get; set; }
    }

    public class Rateinfo
    {
        public string priceBreakdown { get; set; }
        public string promo { get; set; }
        public string rateChange { get; set; }
        public Roomgroup RoomGroup { get; set; }
        public Chargeablerateinfo ChargeableRateInfo { get; set; }
        public string cancellationPolicy { get; set; }
        public Cancelpolicyinfolist CancelPolicyInfoList { get; set; }
        public bool nonRefundable { get; set; }
        public string rateType { get; set; }
        public int promoId { get; set; }
        public string promoDescription { get; set; }
        public string promoType { get; set; }
        public int currentAllotment { get; set; }
        public bool guaranteeRequired { get; set; }
        public bool depositRequired { get; set; }
        public float taxRate { get; set; }
    }

    public class Roomgroup
    {
        public Room[] Room { get; set; }
    }

    public class Room
    {
        public int numberOfAdults { get; set; }
        public int numberOfChildren { get; set; }
        public string rateKey { get; set; }
        public Chargeablenightlyrate[] ChargeableNightlyRates { get; set; }
        public int childAges { get; set; }
    }

    public class Chargeablenightlyrate
    {
        public string baseRate { get; set; }
        public string rate { get; set; }
        public string promo { get; set; }
        public string fenced { get; set; }
    }

    public class Chargeablerateinfo
    {
        public string averageBaseRate { get; set; }
        public string averageRate { get; set; }
        public string commissionableUsdTotal { get; set; }
        public string currencyCode { get; set; }
        public string maxNightlyRate { get; set; }
        public string nightlyRateTotal { get; set; }
        public string grossProfitOffline { get; set; }
        public string grossProfitOnline { get; set; }
        public string surchargeTotal { get; set; }
        public string total { get; set; }
        public Nightlyratesperroom NightlyRatesPerRoom { get; set; }
        public Surcharges Surcharges { get; set; }
    }

    public class Nightlyratesperroom
    {
        public string size { get; set; }
        public Nightlyrate[] NightlyRate { get; set; }
    }

    public class Nightlyrate
    {
        public string baseRate { get; set; }
        public string rate { get; set; }
        public string promo { get; set; }
    }

    public class Surcharges
    {
        public string size { get; set; }
        public object Surcharge { get; set; }
    }

    public class Cancelpolicyinfolist
    {
        public Cancelpolicyinfo[] CancelPolicyInfo { get; set; }
    }

    public class Cancelpolicyinfo
    {
        public int versionId { get; set; }
        public string cancelTime { get; set; }
        public int startWindowHours { get; set; }
        public int nightCount { get; set; }
        public string currencyCode { get; set; }
        public string timeZoneDescription { get; set; }
        public int percent { get; set; }
    }

    public class Valueadds
    {
        public string size { get; set; }
        public object ValueAdd { get; set; }
    }


}
