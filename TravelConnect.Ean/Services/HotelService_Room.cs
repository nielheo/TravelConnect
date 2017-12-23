using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
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
                        $"&{OccupancyToString(request.Occupancies)}",
                        //+     $"&includeDetails=true",
                        RequestType.RoomAvailability);

                    var rs = JsonConvert.DeserializeObject<HotelRoomAvailRS>(response);
                    HotelRoomRS roomResponse = ConvertToHotelRoomRS(rs);//, request, sRequest);

                    _LogService.LogInfo($"EAN/HotelRoomRS", roomResponse);

                    if (roomResponse != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            // Keep in cache for this time, reset time if accessed.
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                        // Save data in cache.
                        _cache.Set(sRequest, roomResponse, cacheEntryOptions);
                    }

                    return new HotelRoomRS();
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

        private HotelRoomRS ConvertToHotelRoomRS(HotelRoomAvailRS response)
        {
            HotelRoomRS rs = new HotelRoomRS();

            return rs;
        }
    }
}