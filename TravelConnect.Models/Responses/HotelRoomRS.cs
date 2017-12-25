using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Models.Responses
{
    [NotMapped]
    public class ChargeableRateRS
    {
        public string Currency { get; set; }
        public decimal TotalCommissionable { get; set; }
        public decimal TotalSurcharge { get; set; }
        public decimal Total { get; set; }
    }

    [NotMapped]
    public class RoomDailyRate
    {
        public decimal BaseRate { get; set; }
        public decimal Rate { get; set; }
        public bool IsPromo { get; set; }
    }

    [NotMapped]
    public class RoomGroupRS
    {
        public int Adult { get; set; }
        public int Child { get; set; }
        public List<int> ChildAges { get; set; }
        public string RateKey { get; set; }
        public List<RoomDailyRate> RoomDailyRates { get; set; }
    }

    [NotMapped]
    public class ImageRS
    {
        public string Url { get; set; }
        public string HighResUrl { get; set; }
        public bool IsHeroImage { get; set; }
        public string Caption { get; set; }
    }

    [NotMapped]
    public class ValueAddRS
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    [NotMapped]
    public class CancellationPolicyRS
    {
        public DateTime CancelTime { get; set; }
        public int StartWindowHours { get; set; }
        public int NightCount { get; set; }
        public decimal Percent { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string TimeZoneDesc { get; set; }
    }

    [NotMapped]
    public class RoomRS
    {
        public string RateCode { get; set; }
        public string RoomTypeId  { get; set; }
        public string RoomCode { get; set; }
        public string RateDesc { get; set; }
        public string RoomTypeDesc { get; set; }
        public string RoomTypeDescLong { get; set; }
        public bool IsNonRefundable { get; set; }
        public bool IsPromo { get; set; }
        public string PromoId { get; set; }
        public string PromoDesc { get; set; }
        public int Allotmnet { get; set; }
        public bool IsGuaranteRequired { get; set; }
        public bool IsDepositRequired { get; set; }
        public bool IsRateChange { get; set; }
        public bool IsPrepaid { get; set; }
        public ChargeableRateRS ChargeableRate { get; set; }
        public string CancellationPolicyDesc { get; set; }
        public List<CancellationPolicyRS> CancellationPolicies { get; set; }
        public List<ValueAddRS> ValueAdds { get; set; }
        public List<RoomGroupRS> RoomGroups { get; set; }
        public List<ImageRS> RoomImages { get; set; }
        public List<IdStringName> RoomAmenities { get; set; }
        public List<IdStringName> BedTypes { get; set; }
    }

    [NotMapped]
    public class IdStringName
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    [NotMapped]
    public class HotelDetailRS
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CheckInInstructions { get; set; }
        public string SpecialCheckInInstructions { get; set; }
        public int NumberOfRooms { get; set; }
        public string CheckInTime { get; set; }
        public string CheckInEndTime { get; set; }
        public string CheckInMinAge { get; set; }
        public string CheckOutTime { get; set; }
        public string PropertyInformation { get; set; }
        public string AreaInformation { get; set; }
        public string PropertyDescription { get; set; }
        public string HotelPolicy { get; set; }
        public string RoomInformation { get; set; }
        public List<IdStringName> PropertyAmenities { get; set; }
        public List<ImageRS> HotelImages { get; set; }
    }

    [NotMapped]
    public class HotelRoomRS
    {
        public int HotelId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public List<RoomOccupancy> Occupancies { get; set; }
        public string Supplier { get; set; }
        public string Locale { get; set; }
        public string Currency { get; set; }
        public HotelDetailRS HotelDetail { get; set; }
        public List<RoomRS> Rooms { get; set; }
    }
}
