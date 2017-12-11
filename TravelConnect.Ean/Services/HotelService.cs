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
    public class HotelService : BaseService, IHotelService
    {
        public async Task<HotelSearchCityRS> HotelSearchByCity(HotelSearchCityRQ request)
        {
            if (!request.Suppliers.Contains("EAN"))
            {
                return new HotelSearchCityRS();
            }


            _LogService = new LogService();

            try
            {
                _LogService.LogInfo("EAN/HotelSearchCityRQ", request);

                var response = await SubmitAsync($"locale=en_US" +
                    $"&numberOfResults=200" +
                    $"&currencyCode=USD" +
                    $"&city={request.City}" +
                    $"&countryCode={request.Country}" +
                    $"&arrivalDate={request.CheckIn.ToString("MM/dd/yyyy")}" +
                    $"&departureDate={request.CheckOut.ToString("MM/dd/yyyy")}" +
                    $"&{OccupancyToString(request.Occupancies)}" +
                    $"&options=HOTEL_SUMMARY",
                    //+     $"&includeDetails=true", 
                    RequestType.HotelList);
                
                var rs = JsonConvert.DeserializeObject<HotelListRs>(response);
                var cityResponse = ConvertToResponse(rs, request);

                _LogService.LogInfo($"EAN/HotelSearchCityRS", cityResponse);

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

        private HotelSearchCityRS ConvertToResponse(HotelListRs rs, HotelSearchCityRQ request)
        {
            HotelSearchCityRS response = new HotelSearchCityRS
            {
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                LocationId = request.LocationId,
                Supplier = "EAN",
                Occupancies = request.Occupancies.ToList(),
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
            return response;
        }

        private string OccupancyToString(List<RoomOccupancy> occupancies)
        {
            List<string> sRooms = new List<string>();

            int idx = 1;

            foreach(RoomOccupancy room in occupancies)
            {
                sRooms.Add($"Room{idx++}={room.AdultCount}{(((room.ChildAges?.Count ?? 0) > 0) ? "," + string.Join(',', room.ChildAges) : "")}");
            }

            return string.Join('&', sRooms);
        }
    }
}
