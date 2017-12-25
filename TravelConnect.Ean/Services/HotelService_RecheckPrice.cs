using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task<HotelRoomRS> HotelRecheckPriceAsync(HotelRecheckPriceRQ request)
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

            try
            {
                _LogService.LogInfo("EAN/HotelRecheckPriceRQ", request);

                var response = await SubmitAsync($"locale={request.Locale ?? "en_US"}" +
                    $"&currencyCode={request.Currency ?? "USD"}" +
                    $"&hotelId={request.HotelId}" +
                    $"&arrivalDate={request.CheckIn.ToString("MM/dd/yyyy")}" +
                    $"&departureDate={request.CheckOut.ToString("MM/dd/yyyy")}" +
                    $"&rateCode={request.RateCode}" +
                    $"&roomTypeCode={request.RoomTypeCode}" +
                    $"&includeDetails=true" +
                    $"&{OccupancyToString(request.Occupancies)}",
                    //+     $"&includeDetails=true",
                    RequestType.RoomAvailability);

                var rs = JsonConvert.DeserializeObject<HotelRoomAvailRS>(response);
                HotelRoomRS roomResponse = ConvertToHotelRoomRS(rs, request);//, request, sRequest);

                _LogService.LogInfo($"EAN/HotelRecheckPriceRS", roomResponse);
                
                return roomResponse;
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "Ean.HotelService.HotelRecheckPriceAsync");
                return new HotelRoomRS();
            }
            finally
            {
                _LogService = null;
            }

        }
    }
}
