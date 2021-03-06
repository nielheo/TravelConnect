﻿using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.CommonServices;
using TravelConnect.Ean.Models.Rooms;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Ean.Services
{
    public partial class HotelService : BaseService, IHotelService
    {
        public async Task<HotelRoomRS> HotelRoomAsync(HotelRoomRQ request)
        {
            if (!request.Suppliers.Contains("EAN"))
            {
                return new HotelRoomRS();
            }

            _LogService = new LogService();

            if (string.IsNullOrEmpty(request.Locale))
                request.Locale = "en_US";

            if (string.IsNullOrEmpty(request.Currency))
                request.Currency = "USD";

            string sRequest = "RoomAvail_Ean_" + CreateMD5(JsonConvert.SerializeObject(request));
            HotelRoomRS cacheRoomRS;

            if (!_cache.TryGetValue(sRequest, out cacheRoomRS))
            {
                try
                {
                    _LogService.LogInfo("EAN/HotelRoomRQ", request);

                    var response = await SubmitAsync($"locale={request.Locale ?? "en_US"}" +
                        $"&currencyCode={request.Currency ?? "USD"}" +
                        $"&hotelId={request.HotelId}" +
                        $"&arrivalDate={request.CheckIn.ToString("MM/dd/yyyy")}" +
                        $"&departureDate={request.CheckOut.ToString("MM/dd/yyyy")}" +
                        $"&includeRoomImages=true" +
                        $"&includeHotelFeeBreakdown=true" +
                        $"&options=HOTEL_DETAILS,ROOM_TYPES,ROOM_AMENITIES,PROPERTY_AMENITIES,HOTEL_IMAGES" +
                        $"&{OccupancyToString(request.Occupancies)}",
                        //+     $"&includeDetails=true",
                        RequestType.RoomAvailability);

                    var rs = JsonConvert.DeserializeObject<HotelRoomAvailRS>(response);
                    HotelRoomRS roomResponse = ConvertToHotelRoomRS(rs, request);//, request, sRequest);

                    _LogService.LogInfo($"EAN/HotelRoomRS", roomResponse);

                    if (roomResponse != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            // Keep in cache for this time, reset time if accessed.
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                        // Save data in cache.
                        _cache.Set(sRequest, roomResponse, cacheEntryOptions);
                    }

                    return roomResponse;
                }
                catch (Exception ex)
                {
                    _LogService.LogException(ex, "Ean.HotelService.HotelRoomAsync");
                    return new HotelRoomRS();
                }
                finally
                {
                    _LogService = null;
                }
            }

            return cacheRoomRS;
        }

        private HotelRoomRS ConvertToHotelRoomRS(HotelRoomAvailRS response, HotelRoomRQ request)
        {
            var hotelResponse = response.HotelRoomAvailabilityResponse;
            HotelRoomRS rs = new HotelRoomRS()
            {
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                Currency = request.Currency,
                Locale = request.Locale,
                HotelId = request.HotelId,
                Occupancies = request.Occupancies,
                Supplier = "EAN",
                Rooms = new System.Collections.Generic.List<RoomRS>()
            };

            rs.HotelDetail = new HotelDetailRS
            {
                Id = hotelResponse.hotelId,
                Name = hotelResponse.hotelName,
                Address = hotelResponse.hotelAddress,
                //StarRating = hotelResponse.HotelDetails
                City = hotelResponse.hotelCity,
                Country = hotelResponse.hotelCountry,
                CheckInInstructions = hotelResponse.checkInInstructions,
                SpecialCheckInInstructions = hotelResponse.specialCheckInInstructions,
                NumberOfRooms = hotelResponse.HotelDetails?.numberOfRooms ?? 0,
                CheckInTime = hotelResponse.HotelDetails?.checkInTime,
                CheckInEndTime = hotelResponse.HotelDetails?.checkInEndTime,
                CheckInMinAge = hotelResponse.HotelDetails?.checkInMinAge,
                CheckOutTime = hotelResponse.HotelDetails?.checkOutTime,
                PropertyInformation = hotelResponse.HotelDetails?.propertyInformation,
                AreaInformation = hotelResponse.HotelDetails?.areaInformation,
                PropertyDescription = hotelResponse.HotelDetails?.propertyDescription,
                HotelPolicy = hotelResponse.HotelDetails?.hotelPolicy,
                RoomInformation = hotelResponse.HotelDetails?.roomInformation,
                
                PropertyAmenities = hotelResponse.PropertyAmenities?.PropertyAmenity.Select(am => new IdStringName
                {
                    Id = am.amenityId.ToString(),
                    Name = am.amenity
                }).ToList(),
                HotelImages = hotelResponse.HotelImages?.HotelImage.Select(img => new ImageRS
                {
                    Url = img.url,
                    HighResUrl = img.highResolutionUrl,
                    IsHeroImage = img.heroImage,
                    Caption = img.caption,
                }).ToList(),
            };

            if (response.HotelRoomAvailabilityResponse.HotelRoomResponse != null)
            {
                foreach (var r in response.HotelRoomAvailabilityResponse.HotelRoomResponse)
                {
                    try
                    {
                        Rateinfo rateInfo = r.RateInfos.RateInfo;
                        Chargeablerateinfo chargeable = rateInfo.ChargeableRateInfo;

                        RoomRS room = new RoomRS
                        {
                            RateCode = r.rateCode.ToString(),
                            RoomTypeId = r.RoomType?.roomTypeId ?? r.roomTypeCode,
                            RoomCode = r.RoomType?.roomCode ?? r.roomTypeCode,
                            RateDesc = r.rateDescription,
                            RoomTypeDesc = r.RoomType?.description ?? r.roomTypeDescription,
                            RoomTypeDescLong = r.RoomType?.descriptionLong,
                            SmokingPreferences = r.smokingPreferences.Split(',').ToList(),
                            IsPromo = rateInfo.promo.ToLower() == "true",
                            PromoId = rateInfo.promoId.ToString(),
                            PromoDesc = rateInfo.promoDescription,
                            Allotmnet = rateInfo.currentAllotment,
                            IsGuaranteRequired = rateInfo.guaranteeRequired,
                            IsDepositRequired = rateInfo.depositRequired,
                            IsNonRefundable = rateInfo.nonRefundable,
                            IsRateChange = rateInfo.rateChange.ToLower() != "false",
                            IsPrepaid = rateInfo.rateType?.ToLower() == "merchantstandard",
                            ChargeableRate = new ChargeableRateRS
                            {
                                Currency = chargeable.currencyCode,
                                Total = Convert.ToDecimal(chargeable.total),
                                TotalCommissionable = Convert.ToDecimal(chargeable.commissionableUsdTotal),
                                TotalSurcharge = Convert.ToDecimal(chargeable.surchargeTotal),
                            },
                            CancellationPolicyDesc = rateInfo.cancellationPolicy,
                            CancellationPolicies = rateInfo.CancelPolicyInfoList.CancelPolicyInfo.Select(cxl =>
                            {
                                string[] cxlTime = cxl.cancelTime.Split(":");
                                DateTime cancelTime = request.CheckIn
                                    .AddHours(Convert.ToInt32(cxlTime[0]))
                                    .AddMinutes(Convert.ToInt32(cxlTime[1]))
                                    .AddSeconds(Convert.ToInt32(cxlTime[2]))
                                    .AddHours(cxl.startWindowHours * -1);


                                return new CancellationPolicyRS
                                {
                                    Currency = cxl.currencyCode,
                                    NightCount = cxl.nightCount,
                                    Percent = cxl.percent,
                                    Amount = cxl.amount,
                                    TimeZoneDesc = cxl.timeZoneDescription,
                                    CancelTime = cancelTime
                                };
                            }).ToList()
                        };

                        room.RoomGroups = rateInfo.RoomGroup.Room.Select(rm => new RoomGroupRS
                        {
                            Adult = rm.numberOfAdults,
                            Child = rm.numberOfChildren,
                            RateKey = rm.rateKey,
                            RoomDailyRates = rm.ChargeableNightlyRates.Select(dr => new RoomDailyRate
                            {
                                BaseRate = Convert.ToDecimal(dr.baseRate),
                                Rate = Convert.ToDecimal(dr.rate),
                                IsPromo = dr.promo.ToLower() == "true"
                            }).ToList()
                        }).ToList();

                        room.ValueAdds = r.ValueAdds?.ValueAdd.Select(va => new ValueAddRS
                        {
                            Id = va.id,
                            Description = va.description
                        }).ToList();

                        room.RoomImages = r.RoomImages?.RoomImage.Select(img => new ImageRS
                        {
                            Url = img.url,
                            HighResUrl = img.highResolutionUrl,
                            IsHeroImage = img.heroImage
                        }).ToList();

                        room.RoomAmenities = r.RoomType?.roomAmenities?.RoomAmenity.Select(am => new IdStringName
                        {
                            Id = am.amenityId,
                            Name = am.amenity
                        }).ToList();

                        room.BedTypes = r.BedTypes?.BedType.Select(bt => new IdStringName
                        {
                            Id = bt.id,
                            Name = bt.description
                        }).ToList();

                        rs.Rooms.Add(room);
                    }
                    catch (Exception ex)
                    {
                        _LogService.LogException(ex, "Ean.HotelService.HotelRoomAsync.AddRoom");
                    }
                }
            }

            return rs;
        }
    }
}