using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelConnect.CommonServices;
using TravelConnect.Gta.DataModels;

namespace TravelConnect.Gta.DataServices
{
    public class HotelRepository : IHotelRepository
    {
        private GtaContext _db { get; set; }

        protected LogService _LogService = null;

        public HotelRepository(GtaContext db, ILogger<GeoRepository> logger)
        {
            _db = db;
        }

        public async Task<Hotel> GetHotel(string code)
        {
            return await _db.Hotels.FirstOrDefaultAsync(h=>h.Code == code);
        }

        public async Task<List<Hotel>> GetHotels(string cityCode)
        {
            return await _db.Hotels.Where(h => h.CityCode == cityCode).ToListAsync();
        }

        public async Task InsertHotel(Hotel hotel)
        {
            try
            {
                var facilities = _db.Facilities.ToList();
                var locations = _db.Locations.ToList();
                var roomTypes = _db.RoomTypes.ToList();

                if (hotel.HotelFacilities != null)
                {
                    hotel.HotelFacilities.ToList().ForEach(f =>
                    {
                        if (facilities.FirstOrDefault(fac => fac.Code.ToUpper() == f.FacilityCode.ToUpper()) == null)
                        {
                            _db.Facilities.Add(new Facility
                            {
                                Code = f.FacilityCode,
                                Name = f.Facility.Name
                            });
                        }
                        f.Facility = null;
                    });
                //    await _db.SaveChangesAsync();
                }

                if (hotel.HotelRoomFacilities != null)
                {
                    hotel.HotelRoomFacilities.ToList().ForEach(f =>
                    {
                        if (facilities.FirstOrDefault(fac => fac.Code.ToUpper() == f.FacilityCode.ToUpper()) == null)
                        {
                            _db.Facilities.Add(new Facility
                            {
                                Code = f.FacilityCode,
                                Name = f.Facility.Name
                            });
                        }
                        f.Facility = null;
                    });
                //    await _db.SaveChangesAsync();
                }

                if (hotel.HotelLocations != null)
                {
                    hotel.HotelLocations.ToList().ForEach(l =>
                    {
                        if (locations.FirstOrDefault(loc => loc.Code.ToUpper() == l.LocationCode.ToUpper()) == null)
                        {
                            _db.Locations.Add(new Location
                            {
                                Code = l.LocationCode,
                                Name = l.Location.Name
                            });
                        }
                        l.Location = null;
                    });
                    
                 //   await _db.SaveChangesAsync();
                }

                if (hotel.HotelRoomTypes != null)
                {
                    hotel.HotelRoomTypes.ToList().ForEach(r =>
                    {
                        if (roomTypes.FirstOrDefault(rt => rt.Code.ToUpper() == r.RoomTypeCode.ToUpper()) == null)
                        {
                            _db.RoomTypes.Add(new RoomType
                            {
                                Code = r.RoomTypeCode,
                                Name = r.RoomType.Name
                            });
                        }
                        r.RoomType = null;
                    });
                //    await _db.SaveChangesAsync();
                }

                var dbHotel = GetHotel(hotel.Code).Result;

                if (dbHotel == null)
                {
                    //_db = new GtaContext();
                    _db.Hotels.Add(hotel);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "Gta.DataServices.HotelRepository.InsertHotel");
            //    throw;
            }
        }

        public async Task InsertHotels(List<Hotel> hotels, string cityCode)
        {
            var dbHotels = await GetHotels(cityCode);

            foreach (var hotel in hotels.Where(h => h.CityCode == cityCode))
            {
                if (!dbHotels.Any(h=>h.Code == hotel.Code))
                {
                    _db.Hotels.Add(hotel);
                }
            }

            await _db.SaveChangesAsync();
        }

        public async Task<Hotel> GetHotelWithDetails(string code)
        {
            try
            {
                return await _db.Hotels
                    .Include(c => c.HotelAreaDetails)
                    .Include(c => c.HotelFacilities).ThenInclude(ad => ad.Facility)
                    .Include(c => c.HotelImageLinks)
                    .Include(c => c.HotelLocations).ThenInclude(hl => hl.Location)
                    .Include(c => c.HotelMapLinks)
                    .Include(c => c.HotelReports)
                    .Include(c => c.HotelRoomCategories)
                    .Include(c => c.HotelRoomFacilities).ThenInclude(ad => ad.Facility)
                    .Include(c => c.HotelRoomTypes).ThenInclude(ad => ad.RoomType)
                    .FirstOrDefaultAsync(h => h.Code == code);
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "Gta.HotelRepository.GetHotelWIthDetails");
                return new Hotel();
            }
        }

        public async Task<List<Hotel>> GetHotelsWithDetails(string cityCode)
        {
            return await _db.Hotels
                .Include(c => c.HotelAreaDetails)
                .Include(c => c.HotelFacilities).ThenInclude(ad => ad.Facility)
                .Include(c => c.HotelImageLinks)
                .Include(c => c.HotelLocations).ThenInclude(hl => hl.Location)
                .Include(c => c.HotelMapLinks)
                .Include(c => c.HotelReports)
                .Include(c => c.HotelRoomCategories)
                .Include(c => c.HotelRoomFacilities).ThenInclude(ad => ad.Facility)
                .Include(c => c.HotelRoomTypes).ThenInclude(ad => ad.RoomType)
                .Where(h => h.CityCode == cityCode)
                .ToListAsync();
        }

        public async Task<Hotel> GetHotelWithImages(string code)
        {
            try
            {
                return await _db.Hotels
                    .Include(c => c.HotelImageLinks)
                    .FirstOrDefaultAsync(h => h.Code == code);
            }
            catch (Exception ex)
            {
                _LogService.LogException(ex, "Gta.HotelRepository.GetHotelWithImages");
                return null;
            }
        }
    }
}