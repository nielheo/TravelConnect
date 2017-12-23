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
    public class RoomImageRS
    {
        public string Url { get; set; }
        public string HighResUrl { get; set; }
        public bool IsHeroImage { get; set; }
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
        public bool IsPromo { get; set; }
        public string PromoId { get; set; }
        public string PromoDesc { get; set; }
        public int Allotmnet { get; set; }
        public bool IsGuaranteRequired { get; set; }
        public bool IsDepositRequired { get; set; }
        public ChargeableRateRS ChargeableRate { get; set; }
        public List<RoomGroupRS> RoomGroups { get; set; }
        public List<RoomImageRS> RoomImages { get; set; }
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
        public List<RoomRS> Rooms { get; set; }
        public string CheckInInstructions { get; set; }
        public string SpecialCheckInInstructions { get; set; }
    }
}
