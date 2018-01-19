using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Gta.Models;
using TravelConnect.Interfaces;
using TravelConnect.Models;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Gta.Services
{
    public partial class HotelService : BaseService, IHotelService
    {
        private bool IsValidRequest(HotelSearchCityRQ request)
        {
            if (!request.Suppliers.Contains("GTA"))
                return false;

            foreach (var occ in request.Occupancies)
            {
                if (occ.AdultCount > 2)
                    return false;
                if (occ.ChildAges != null)
                {
                    if (occ.ChildAges.Count() > 1)
                        return false;
                }
            }

            return true;
        }

        public async Task<HotelSearchCityRS> HotelSearchByCityAsync(HotelSearchCityRQ request)
        {
            try
            {
                _LogService = new CommonServices.LogService();
                _LogService.LogInfo("GTA/HotelSearchCityRQ", request);

                if (!IsValidRequest(request))
                    return null;

                SearchHotelPriceRequest req = ConvertToSearchHotelPriceRequest(request);

                var xRequest = Serialize(req);

                var result = await SubmitAsync(xRequest);

                var response = Deserialize<SearchHotelPriceResponse>(result);

                var rsp = ConvertToHotelSearchCityRS(response, request);

                _LogService.LogInfo("GTA/HotelSearchCityRS", rsp);

                return await rsp;
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "Gta.HotelService.HotelSearchByCity");
                return new HotelSearchCityRS();
            }
        }

        private async Task<HotelSearchCityRS> ConvertToHotelSearchCityRS(SearchHotelPriceResponse response,
            HotelSearchCityRQ request)
        {
            var hotelCodes = response.ResponseDetails.SearchHotelPriceResponse.HotelDetails
                .Select(htl => $"{htl.City.Code}.{htl.Item.Code}").ToList();

            var dbHotels = await _HotelRepository.GetHotelsWithImages(hotelCodes);

            HotelSearchCityRS res = new HotelSearchCityRS
            {
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                Supplier = "GTA",
                Hotels = response.ResponseDetails.SearchHotelPriceResponse.HotelDetails.Select(htl =>
                {
                    var dbHotel = dbHotels.FirstOrDefault(h => h.Code == $"{htl.City.Code}.{htl.Item.Code}");


                    return new HotelRS
                    {
                        Id = $"{htl.City.Code}.{htl.Item.Code}",
                        Name = htl.Item.Value,
                        Address = dbHotel?.Address1 ?? "",
                        Thumbnail = dbHotel?.HotelImageLinks?.FirstOrDefault()?.Thumbnail,
                        StarRating = htl.StarRating,
                        CurrCode = htl.RoomCategories.First().ItemPrice.Currency,
                        RateFrom = htl.RoomCategories.Min(r => r.ItemPrice.Value),
                        RateTo = htl.RoomCategories.Max(r => r.ItemPrice.Value),
                        CityCode = htl.City.Code,
                        CityName = htl.City.Value,
                        HotelRooms = htl.RoomCategories.Select(rm =>
                            new RoomListRS
                            {
                                Allotment = request.Occupancies.Count(),
                                RoomTypeCode = rm.Id,
                                RateCode = rm.Id,
                                PromoDesc = rm.Description,
                                ChargeableRate = new ChargeableRateRS
                                {
                                    Currency = rm.ItemPrice.Currency,
                                    Total = rm.ItemPrice.Value,
                                    MaxNightlyRate = rm.ItemPrice.Value / (decimal)(request.CheckOut - request.CheckIn).TotalDays
                                }
                            }
                           ).ToList()
                    };
                }).ToList()
            };
            
            return res;
        }

        private string AdultToRoomCode(int AdultCount)
        {
            switch (AdultCount)
            {
                case 1:
                    return "SB";

                case 2:
                default:
                    return "TB";
            }
        }

        private Room[] ConvertToRoom(List<RoomOccupancy> Occupancies)
        {
            return Occupancies.Select(occ => new Room
            {
                Code = AdultToRoomCode(occ.AdultCount),
                ExtraBeds = occ.ChildAges == null ? null : new Extrabeds
                {
                    Age = occ.ChildAges.First().ToString()
                }
            }).ToArray();
        }

        private SearchHotelPriceRequest ConvertToSearchHotelPriceRequest(HotelSearchCityRQ request)
        {
            SearchHotelPriceRequest req = new SearchHotelPriceRequest
            {
                Source = Source,
                RequestDetails = new Requestdetails
                {
                    SearchHotelPriceRequest = new Searchhotelpricerequest
                    {
                        ItemDestination = new Itemdestination
                        {
                            DestinationType = "city",
                            DestinationCode = request.City
                        },
                        ImmediateConfirmationOnly = "",
                        PeriodOfStay = new Periodofstay
                        {
                            CheckInDate = request.CheckIn.ToString("yyyy-MM-dd"),
                            Duration = (int)(request.CheckOut.Date - request.CheckIn.Date).TotalDays,
                        },
                        Rooms = ConvertToRoom(request.Occupancies),
                        OrderBy = "pricelowtohigh",
                        NumberOfReturnedItems = "2000"
                    },
                }
            };

            return req;
        }
    }
}