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

            string hotelCode = $"{itemDetail.City.Code.ToUpper() }.{itemDetail.Item.Code.ToUpper() }";

            Hotel hotel = new Hotel
            {
                Code = hotelCode,
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
                        HotelCode = hotelCode,
                        Caption = img.Text,
                        Thumbnail = img.ThumbNail,
                        Image = img.Image
                    }
                ).ToList(),
                HotelAreaDetails = itemDetail.HotelInformation.AreaDetails.Select(ad =>
                    new HotelAreaDetail
                    {
                        HotelCode = hotelCode,
                        AreaDetail = ad,
                    }
                ).ToList(),
                HotelFacilities = itemDetail.HotelInformation.Facilities.Select(fa =>
                    new HotelFacility
                    {
                        HotelCode = hotelCode,
                        FacilityCode = fa.Code,
                        Facility = new Facility
                        {
                            Name = fa.Value
                        }
                    }
                ).ToList(),
                HotelLocations = new List<HotelLocation> {
                    new HotelLocation
                    {
                        HotelCode = hotelCode,
                        LocationCode = itemDetail.LocationDetails.Location.Code,
                        Location = new Location
                        {
                            Name = itemDetail.LocationDetails.Location.Value
                        }
                    }
                },
                HotelMapLinks = new List<HotelMapLink>
                {
                    new HotelMapLink
                    {
                        HotelCode = hotelCode,
                        MapLink = itemDetail.HotelInformation.Links.MapLinks.MapPageLink
                    }
                },
                HotelReports = itemDetail.HotelInformation.Reports.Select(rpt =>
                    new HotelReport
                    {
                        HotelCode = hotelCode,
                        Report = rpt.Value,
                        Type = rpt.Type
                    }
                ).ToList(),
                HotelRoomCategories = itemDetail.HotelInformation.RoomCategories.Select(rm =>
                    new HotelRoomCategory
                    {
                        HotelCode = hotelCode,
                        Id = rm.Id,
                        Name = rm.Description,
                        Description = rm.RoomDescription,
                    }
                ).ToList(),
                HotelRoomFacilities = itemDetail.HotelInformation.RoomFacilities.Select(fac =>
                    new HotelRoomFacility
                    {
                        HotelCode = hotelCode,
                        FacilityCode = fac.Code,
                        Facility = new Facility
                        {
                            Name = fac.Value
                        }
                    }
                ).ToList(),
                HotelRoomTypes = itemDetail.HotelInformation.RoomTypes.RoomType.Select(rt =>
                    new HotelRoomType
                    {
                        HotelCode = hotelCode,
                        RoomTypeCode = rt.Code,
                        RoomType = new RoomType
                        {
                            Name = rt.Value
                        }
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