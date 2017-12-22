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

        public async Task<HotelSearchCityRS> HotelGetMoreAsync(HotelGetMoreRQ request)
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

            try
            {
                _LogService.LogInfo("EAN/HotelGetMoreRQ", request);

                var response = await SubmitAsync($"locale={request.Locale ?? "en_US"}" +
                    $"&currencyCode={request.Currency ?? "USD"}" +
                    $"&cachekey={request.CacheKey}" +
                    $"&cachelocation={request.CacheLocation}" +
                    $"&options=HOTEL_SUMMARY",
                    //+     $"&includeDetails=true",
                    RequestType.HotelList);

                var rs = JsonConvert.DeserializeObject<HotelListRs>(response);
                HotelSearchCityRS hotelSearchCityRS = ConvertToResponse(rs, request);

                _LogService.LogInfo($"EAN/HotelGetMoreRS", response);

                //Add result to cache

                HotelSearchCityRS cacheSearchRS;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                            // Keep in cache for this time, reset time if accessed.
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                if (!_cache.TryGetValue(request.RequestKey, out cacheSearchRS))
                {
                    _cache.Set(request.RequestKey, hotelSearchCityRS, cacheEntryOptions);
                }
                else
                {
                    hotelSearchCityRS.Hotels.ForEach(hotel =>
                    {
                        if (cacheSearchRS.Hotels.FirstOrDefault(h => h.Id == hotel.Id) == null)
                        {
                            cacheSearchRS.Hotels.Add(hotel);
                        }
                    });

                    _cache.Set(request.RequestKey, cacheSearchRS, cacheEntryOptions);
                }
                return hotelSearchCityRS;
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "Ean.HotelService.HotelGetMoreAsync");
                return new HotelSearchCityRS();
            }
            finally
            {
                _LogService = null;
            }
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
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
                        $"&options=HOTEL_SUMMARY",
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

                    //if (rs.HotelListResponse.moreResultsAvailable)
                    //{
                    //    var getMoreReq = new HotelGetMoreRQ
                    //    {
                    //        CacheKey = rs.HotelListResponse.cacheKey,
                    //        CacheLocation = rs.HotelListResponse.cacheLocation,
                    //        Currency = request.Currency,
                    //        Locale = request.Locale,
                    //        RequestKey = sRequest,
                    //        Suppliers = new List<string> { "EAN" }
                    //    };

                    //    var more = HotelGetMoreAsync(getMoreReq).Result;
                    //}

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
                        RateTo = (decimal)h.highRate
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