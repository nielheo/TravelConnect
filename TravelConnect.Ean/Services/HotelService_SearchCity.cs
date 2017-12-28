using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.CommonServices;
using TravelConnect.Ean.Models;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Ean.Services
{
    public partial class HotelService : BaseService, IHotelService
    {
        private IMemoryCache _cache;

        public HotelService(
                IMemoryCache memoryCache)
        {
            this._cache = memoryCache;
        }
        
        public async Task<HotelSearchCityRS> HotelSearchByCityAsync(HotelSearchCityRQ request)
        {
            if (!request.Suppliers.Contains("EAN"))
            {
                return new HotelSearchCityRS();
            }

            _LogService = new LogService();

            if (string.IsNullOrEmpty(request.Locale))
                request.Locale = "en_US";

            if (string.IsNullOrEmpty(request.Currency))
                request.Currency = "USD";

            string sRequest = "HotelSearch_Ean_" + CreateMD5(JsonConvert.SerializeObject(request));
            HotelSearchCityRS cacheSearchRS;

            if (!_cache.TryGetValue(sRequest, out cacheSearchRS))
            {
                try
                {
                    _LogService.LogInfo("EAN/HotelSearchCityRQ", request);

                    var response = await SubmitAsync($"locale={request.Locale ?? "en_US"}" +
                        $"&numberOfResults=200" +
                        $"&currencyCode={request.Currency ?? "USD"}" +
                        $"&city={request.City}" +
                        $"&countryCode={request.Country}" +
                        $"&arrivalDate={request.CheckIn.ToString("MM/dd/yyyy")}" +
                        $"&departureDate={request.CheckOut.ToString("MM/dd/yyyy")}" +
                        $"&{OccupancyToString(request.Occupancies)}" +
                        $"&maxRatePlanCount=3",
                        //+     $"&includeDetails=true",
                        RequestType.HotelList);

                    var rs = JsonConvert.DeserializeObject<HotelListRs>(response);
                    HotelSearchCityRS cityResponse = ConvertToResponse(rs, request, sRequest);

                    _LogService.LogInfo($"EAN/HotelSearchCityRS", cityResponse);

                    if (cityResponse != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            // Keep in cache for this time, reset time if accessed.
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                        // Save data in cache.
                        _cache.Set(sRequest, cityResponse, cacheEntryOptions);
                    }
                    
                    return cityResponse;
                }
                catch (Exception ex)
                {
                    _LogService.LogException(ex, "Ean.HotelService.HotelSearchByCity");
                    return new HotelSearchCityRS();
                }
                finally
                {
                    _LogService = null;
                }
            }
            else
            {
                cacheSearchRS.CacheKey = "";
                cacheSearchRS.CacheLocation = "";
                cacheSearchRS.RequestKey = "";
            }

            return cacheSearchRS;
        }

        private HotelSearchCityRS ConvertToResponse(HotelListRs rs,
            HotelSearchCityRQ request, string requestKey)
        {
            HotelSearchCityRS response = new HotelSearchCityRS
            {
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                LocationId = request.LocationId,
                Supplier = "EAN",
                Occupancies = request.Occupancies.ToList(),
                Locale = request.Locale,
                Currency = request.Currency,
                Hotels = rs.HotelListResponse.HotelList.HotelSummary.ToList().Select(h =>
                    new HotelRS
                    {
                        Id = h.hotelId.ToString(),
                        Name = h.name,
                        Address = h.address1,
                        Latitude = h.latitude,
                        Longitude = h.longitude,
                        Location = h.locationDescription,
                        ShortDesc = h.shortDescription,
                        StarRating = (decimal)h.hotelRating,
                        Thumbnail = h.thumbNailUrl,
                        CurrCode = h.rateCurrencyCode,
                        RateFrom = (decimal)h.lowRate,
                        RateTo = (decimal)h.highRate,
                        HotelRooms = h.RoomRateDetailsList.RoomRateDetails.Select(rm =>
                        new RoomListRS
                        {
                            RoomTypeCode = rm.roomTypeCode.ToString(),
                            RateCode = rm.rateCode.ToString(),
                            PromoDesc = rm.RateInfos.RateInfo.promoDescription,
                            Allotment = rm.RateInfos.RateInfo.currentAllotment,
                            ChargeableRate = new ChargeableRateRS
                            {
                                Currency = rm.RateInfos.RateInfo.ChargeableRateInfo.currencyCode,
                                MaxNightlyRate = Convert.ToDecimal(rm.RateInfos.RateInfo.ChargeableRateInfo.maxNightlyRate),
                                TotalCommissionable = Convert.ToDecimal(rm.RateInfos.RateInfo.ChargeableRateInfo.commissionableUsdTotal),
                                TotalSurcharge = Convert.ToDecimal(rm.RateInfos.RateInfo.ChargeableRateInfo.surchargeTotal),
                                Total = Convert.ToDecimal(rm.RateInfos.RateInfo.ChargeableRateInfo.total),
                            }
                        }).OrderBy(r => r.ChargeableRate.Total).ToList()
                    }
                ).ToList()
            };

            if (rs.HotelListResponse.moreResultsAvailable)
            {
                response.CacheKey = rs.HotelListResponse.cacheKey;
                response.CacheLocation = rs.HotelListResponse.cacheLocation;
                response.RequestKey = requestKey;
            }

            return response;
        }

        private HotelSearchCityRS ConvertToResponse(HotelListRs rs,
            HotelGetMoreRQ request)
        {
            HotelSearchCityRS response = new HotelSearchCityRS
            {
                Supplier = "EAN",
                Locale = request.Locale,
                Currency = request.Currency,
                Hotels = rs.HotelListResponse.HotelList.HotelSummary.ToList().Select(h =>
                    new HotelRS
                    {
                        Id = h.hotelId.ToString(),
                        Name = h.name,
                        Address = h.address1,
                        Latitude = h.latitude,
                        Longitude = h.longitude,
                        Location = h.locationDescription,
                        ShortDesc = h.shortDescription,
                        StarRating = (decimal)h.hotelRating,
                        Thumbnail = h.thumbNailUrl,
                        CurrCode = h.rateCurrencyCode,
                        RateFrom = (decimal)h.lowRate,
                        RateTo = (decimal)h.highRate
                    }
                ).ToList()
            };

            if (rs.HotelListResponse.moreResultsAvailable)
            {
                response.CacheKey = rs.HotelListResponse.cacheKey;
                response.CacheLocation = rs.HotelListResponse.cacheLocation;
                response.RequestKey = request.RequestKey;
            }

            return response;
        }

        private string OccupancyToString(List<RoomOccupancy> occupancies)
        {
            List<string> sRooms = new List<string>();

            int idx = 1;

            foreach (RoomOccupancy room in occupancies)
            {
                sRooms.Add($"Room{idx++}={room.AdultCount}{(((room.ChildAges?.Count ?? 0) > 0) ? "," + string.Join(',', room.ChildAges) : "")}");
            }

            return string.Join('&', sRooms);
        }
    }
}
 