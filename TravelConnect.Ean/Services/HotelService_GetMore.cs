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
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Ean.Services
{
    public partial class HotelService : BaseService, IHotelService
    {
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

    }
}
