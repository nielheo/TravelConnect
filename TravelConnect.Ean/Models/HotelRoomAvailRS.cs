using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public string specialCheckInInstructions { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<Hotelroomresponse>))]
        public List<Hotelroomresponse> HotelRoomResponse { get; set; }
        public Hoteldetails HotelDetails { get; set; }
        public Propertyamenities PropertyAmenities { get; set; }
        public Hotelimages HotelImages { get; set; }
    }

    public class Hoteldetails
    {
        public int numberOfRooms { get; set; }
        public int numberOfFloors { get; set; }
        public string checkInTime { get; set; }
        public string checkInEndTime { get; set; }
        public string checkInMinAge { get; set; }
        public string checkOutTime { get; set; }
        public string propertyInformation { get; set; }
        public string areaInformation { get; set; }
        public string propertyDescription { get; set; }
        public string hotelPolicy { get; set; }
        public string roomInformation { get; set; }
        public string checkInInstructions { get; set; }
        public string knowBeforeYouGoDescription { get; set; }
        public string roomFeesDescription { get; set; }
        public string locationDescription { get; set; }
        public string diningDescription { get; set; }
        public string amenitiesDescription { get; set; }
        public string businessAmenitiesDescription { get; set; }
        public string roomDetailDescription { get; set; }
    }

    public class Propertyamenities
    {
        public string size { get; set; }
        public Propertyamenity[] PropertyAmenity { get; set; }
    }

    public class Propertyamenity
    {
        public int amenityId { get; set; }
        public string amenity { get; set; }
    }

    public class Hotelimages
    {
        public string size { get; set; }
        public Hotelimage[] HotelImage { get; set; }
    }

    public class Hotelimage
    {
        public int hotelImageId { get; set; }
        public string name { get; set; }
        public int category { get; set; }
        public int type { get; set; }
        public string caption { get; set; }
        public string url { get; set; }
        public string highResolutionUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public int supplierId { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int byteSize { get; set; }
        public bool heroImage { get; set; }
    }

    public class Hotelroomresponse
    {
        public int rateCode { get; set; }
        public string rateDescription { get; set; }
        public Roomtype RoomType { get; set; }
        public string roomTypeCode { get; set; }
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
        public Roomimages RoomImages { get; set; }
    }

    public class Roomtype
    {
        [JsonProperty(PropertyName = "@roomTypeId")]
        public string roomTypeId { get; set; }

        [JsonProperty(PropertyName = "@roomCode")]
        public string roomCode { get; set; }

        public string description { get; set; }
        public string descriptionLong { get; set; }
        public Roomamenities roomAmenities { get; set; }
    }

    public class Roomamenities
    {
        public string size { get; set; }
        public Roomamenity[] RoomAmenity { get; set; }
    }

    public class Roomamenity
    {
        [JsonProperty(PropertyName = "@amenityId")]
        public string amenityId { get; set; }
        public string amenity { get; set; }
    }

    public class Bedtype
    {
        [JsonProperty(PropertyName = "@id")]
        public string id { get; set; }

        public string description { get; set; }
    }
    
    public class Bedtypes
    {
        public string size { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<Bedtype>))]
        public List<Bedtype> BedType { get; set; }
    }

    public class Rateinfos
    {
        [JsonProperty(PropertyName = "@size")]
        public string size { get; set; }
        public Rateinfo RateInfo { get; set; }
    }

    public class Rateinfo
    {
        [JsonProperty(PropertyName = "@priceBreakdown")]
        public string priceBreakdown { get; set; }

        [JsonProperty(PropertyName = "@promo")]
        public string promo { get; set; }

        [JsonProperty(PropertyName = "@rateChange")]
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


    public class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objecType)
        {
            return (objecType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objecType, object existingValue,
            JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class Roomgroup
    {
        [JsonConverter(typeof(SingleOrArrayConverter<Room>))]
        public List<Room> Room { get; set; }
    }

    public class Room
    {
        public int numberOfAdults { get; set; }
        public int numberOfChildren { get; set; }
        public string childAges { get; set; }
        public string rateKey { get; set; }
        public Chargeablenightlyrate[] ChargeableNightlyRates { get; set; }
    }

    public class Chargeablenightlyrate
    {
        [JsonProperty(PropertyName = "@baseRate")]
        public string baseRate { get; set; }

        [JsonProperty(PropertyName = "@rate")]
        public string rate { get; set; }

        [JsonProperty(PropertyName = "@promo")]
        public string promo { get; set; }

        [JsonProperty(PropertyName = "@fenced")]
        public string fenced { get; set; }
    }

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

        [JsonProperty(PropertyName = "@grossProfitOffline")]
        public string grossProfitOffline { get; set; }

        [JsonProperty(PropertyName = "@grossProfitOnline")]
        public string grossProfitOnline { get; set; }

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
        public Nightlyrate[] NightlyRate { get; set; }
    }

    public class Nightlyrate
    {
        public string baseRate { get; set; }
        public string rate { get; set; }
        public string promo { get; set; }
        public string fenced { get; set; }
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
        public int percent { get; set; }
        public decimal amount { get; set; }
        public string currencyCode { get; set; }
        public string timeZoneDescription { get; set; }
    }

    public class Valueadd
    {
        [JsonProperty(PropertyName = "@id")]
        public int id { get; set; }
        public string description { get; set; }
    }
    
    public class Valueadds
    {
        public string size { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<Valueadd>))]
        public List<Valueadd> ValueAdd { get; set; }
    }

    public class Roomimages
    {
        public string size { get; set; }
        public Roomimage[] RoomImage { get; set; }
    }

    public class Roomimage
    {
        public string url { get; set; }
        public string highResolutionUrl { get; set; }
        public bool heroImage { get; set; }
    }

}
