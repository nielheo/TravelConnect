﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TravelConnect.Ean.Models.Rooms;

namespace TravelConnect.Ean.Models
{
    public class HotelListRs
    {
        public Hotellistresponse HotelListResponse { get; set; }
    }

    public class Hotellistresponse
    {
        public string customerSessionId { get; set; }
        public int numberOfRoomsRequested { get; set; }
        public bool moreResultsAvailable { get; set; }
        public string cacheKey { get; set; }
        public string cacheLocation { get; set; }
        public Cachedsupplierresponse cachedSupplierResponse { get; set; }
        public Hotellist HotelList { get; set; }
    }

    public class Cachedsupplierresponse
    {
        public string supplierCacheTolerance { get; set; }
        public string cachedTime { get; set; }
        public string candidatePreptime { get; set; }
        public string otherOverheadTime { get; set; }
        public string tpidUsed { get; set; }
        public string matchedCurrency { get; set; }
        public string matchedLocale { get; set; }
    }

    public class Hotellist
    {
        public string size { get; set; }
        public string activePropertyCount { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<Hotelsummary>))]
        public List<Hotelsummary> HotelSummary { get; set; }
    }

    public class Hotelsummary
    {
        public string order { get; set; }
        public string ubsScore { get; set; }
        public int hotelId { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public object postalCode { get; set; }
        public string countryCode { get; set; }
        public string airportCode { get; set; }
        public string supplierType { get; set; }
        public int propertyCategory { get; set; }
        public float hotelRating { get; set; }
        public string hotelRatingDisplay { get; set; }
        public int confidenceRating { get; set; }
        public int amenityMask { get; set; }
        public string locationDescription { get; set; }
        public string shortDescription { get; set; }
        public float highRate { get; set; }
        public float lowRate { get; set; }
        public string rateCurrencyCode { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float proximityDistance { get; set; }
        public string proximityUnit { get; set; }
        public bool hotelInDestination { get; set; }
        public string thumbNailUrl { get; set; }
        public string deepLink { get; set; }
        public Roomratedetailslist RoomRateDetailsList { get; set; }
    }

    public class Roomratedetailslist
    {
        [JsonConverter(typeof(SingleOrArrayConverter<Roomratedetail>))]
        public List<Roomratedetail> RoomRateDetails { get; set; }
    }

    public class Roomratedetail
    {
        public int roomTypeCode { get; set; }
        public int rateCode { get; set; }
        public int maxRoomOccupancy { get; set; }
        public int quotedRoomOccupancy { get; set; }
        public int minGuestAge { get; set; }
        public string roomDescription { get; set; }
        public bool propertyAvailable { get; set; }
        public bool propertyRestricted { get; set; }
        public int expediaPropertyId { get; set; }
        public Rateinfos RateInfos { get; set; }
        public Valueadds ValueAdds { get; set; }
        public Bedtypes BedTypes { get; set; }
        public string smokingPreferences { get; set; }
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
        //public Roomgroup RoomGroup { get; set; }
        public Chargeablerateinfo ChargeableRateInfo { get; set; }
        public string cancellationPolicy { get; set; }
        public Cancelpolicyinfolist CancelPolicyInfoList { get; set; }
        public bool nonRefundable { get; set; }
        public string rateType { get; set; }
        public int currentAllotment { get; set; }
        public int promoId { get; set; }
        public string promoDescription { get; set; }
        public string promoType { get; set; }
    }

    //public class Roomgroup
    //{
    //    public Room[] Room { get; set; }
    //}

    //public class Room
    //{
    //    public int numberOfAdults { get; set; }
    //    public int numberOfChildren { get; set; }
    //    public string rateKey { get; set; }
    //    public Chargeablenightlyrate[] ChargeableNightlyRates { get; set; }
    //    public int[] childAges { get; set; }
    //}

    //public class Chargeablenightlyrate
    //{
    //    public string baseRate { get; set; }
    //    public string rate { get; set; }
    //    public string promo { get; set; }
    //    public string fenced { get; set; }
    //}

    public class Chargeablerateinfo
    {
        [JsonProperty(PropertyName = "@averageBaseRate")]
        public string averageBaseRate { get; set; }

        [JsonProperty(PropertyName = "@averageRate")]
        public string averageRate { get; set; }

        [JsonProperty(PropertyName = "@commissionableUsdTotal")]
        public string commissionableUsdTotal { get; set; }

        [JsonProperty(PropertyName = "@currencyCode")]
        public string currencyCode { get; set; }

        [JsonProperty(PropertyName = "@maxNightlyRate")]
        public string maxNightlyRate { get; set; }

        [JsonProperty(PropertyName = "@nightlyRateTotal")]
        public string nightlyRateTotal { get; set; }

        [JsonProperty(PropertyName = "@surchargeTotal")]
        public string surchargeTotal { get; set; }

        [JsonProperty(PropertyName = "@total")]
        public string total { get; set; }

        public Nightlyratesperroom NightlyRatesPerRoom { get; set; }
        public Surcharges Surcharges { get; set; }
    }

    public class Nightlyratesperroom
    {
        public string size { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<Nightlyrate>))]
        public List<Nightlyrate> NightlyRate { get; set; }
    }

    public class Nightlyrate
    {
        [JsonProperty(PropertyName = "@baseRate")]
        public string baseRate { get; set; }

        [JsonProperty(PropertyName = "@rate")]
        public string rate { get; set; }

        [JsonProperty(PropertyName = "@promo")]
        public string promo { get; set; }
    }

    public class Surcharges
    {
        public string size { get; set; }
        public object Surcharge { get; set; }
    }

    public class Cancelpolicyinfolist
    {
        [JsonConverter(typeof(SingleOrArrayConverter<Cancelpolicyinfo>))]
        public List<Cancelpolicyinfo> CancelPolicyInfo { get; set; }
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

    public class Bedtypes
    {
        public string size { get; set; }
        public object BedType { get; set; }
    }


}
