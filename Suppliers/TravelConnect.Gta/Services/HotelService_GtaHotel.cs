using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;
using TravelConnect.Gta.Interfaces;
using TravelConnect.Gta.Models;

namespace TravelConnect.Gta.Services
{
    public partial class HotelService : BaseService, IGtaHotelService
    {
        private Hotel ConvertToHotel(SearchItemInformationResponse response)
        {
            var itemDetail = response.ResponseDetails.SearchItemInformationResponse.ItemDetails.ItemDetail;

            if (itemDetail == null)
                return null;

            Hotel hotel = new Hotel
            {
                Code = $"{itemDetail.City.Code.ToUpper() }.{itemDetail.Item.Code.ToUpper() }",
                Name = itemDetail.Item.Value,
                Address1 = itemDetail.HotelInformation.AddressLines?.AddressLine1,
                Address2 = itemDetail.HotelInformation.AddressLines?.AddressLine2,
                Address3 = itemDetail.HotelInformation.AddressLines?.AddressLine3,
                Address4 = itemDetail.HotelInformation.AddressLines?.AddressLine4,
                Latitude = (float?)itemDetail.HotelInformation.GeoCodes?.Latitude,
                Longitude = (float?)itemDetail.HotelInformation.GeoCodes?.Longitude,
                CityCode = itemDetail.City.Code.ToUpper(),
                StarRating = itemDetail.HotelInformation.StarRating,
                Email = itemDetail.HotelInformation.AddressLines?.EmailAddress,
                Fax = itemDetail.HotelInformation.AddressLines?.Fax,
                Website = itemDetail.HotelInformation.AddressLines?.WebSite,
                Copyright = itemDetail.Copyright,
                Phone = itemDetail.HotelInformation.AddressLines?.Telephone,
                HotelImageLinks = itemDetail.HotelInformation.Links.ImageLinks.Select(img => 
                    new HotelImageLink
                    {
                        Caption = img.Text,
                         Thumbnail = img.ThumbNail,
                         Image= img.Image
                    }
                ).ToList()
            };

            return hotel;
        }

        public async Task<List<Hotel>> GetHotels(string cityCode, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<Hotel> GetHotel(string code, bool forceRefresh = false)
        {
            code = code.ToUpper();
            string[] codes = code.Split(".");

            if (codes.Length != 2)
                return null;

            try
            {
                _LogService = new CommonServices.LogService();
                _LogService.LogInfo("GTA/SearchHotelRQ", $"Code: {code}");

                SearchItemInformationRequest req = new SearchItemInformationRequest
                {
                    Source = Source,
                    RequestDetails = new Requestdetails
                    {
                        SearchItemInformationRequest = new Searchiteminformationrequest
                        {
                            ItemDestination = new Itemdestination
                            {
                                DestinationCode = codes[0],
                                DestinationType = "city"
                            },
                            ItemCode = codes[1],
                            ItemType = "hotel",
                        }
                    }
                };

                var xRequest = Serialize(req);

                var result = await SubmitAsync(xRequest);

                var response = Deserialize<SearchItemInformationResponse>(result);

                Hotel hotel = ConvertToHotel(response);

                if (hotel == null)
                {
                    _LogService.LogInfo("GTA/SearchHotelRQ", "null");
                    return null;
                }

                _LogService.LogInfo("GTA/SearchHotelRQ", hotel);

                return hotel;
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "Gta.GeoService.GetGtaCities");
                return null;
            }
        }
    }
}