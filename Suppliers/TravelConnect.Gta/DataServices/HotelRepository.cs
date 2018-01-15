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

        public async void InsertHotel(Hotel hotel)
        {
            var dbHotel = await GetHotel(hotel.Code);

            if (dbHotel == null)
            {
                _db.Hotels.Add(hotel);
                await _db.SaveChangesAsync();
            }
        }

        public async void InsertHotels(List<Hotel> hotels, string cityCode)
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
            return await _db.Hotels
                .Include(c => c.HotelAreaDetails.Select(ad=>ad.AreaDetail))
                .Include(c => c.HotelFacilities .Select(ad => ad.Facility))
                .Include(c => c.HotelImageLinks)
                .Include(c => c.HotelLocations.Select(ad => ad.Location))
                .Include(c => c.HotelMapLinks.Select(ad => ad.MapLink))
                .Include(c => c.HotelReports)
                .Include(c => c.HotelRoomCategories)
                .Include(c => c.HotelRoomFacilities.Select(ad => ad.Facility))
                .Include(c => c.HotelRoomTypes.Select(ad => ad.RoomType))
                .FirstOrDefaultAsync(h => h.Code == code);
        }

        public async Task<List<Hotel>> GetHotelsWithDetails(string cityCode)
        {
            return await _db.Hotels
                .Include(c => c.HotelAreaDetails.Select(ad => ad.AreaDetail))
                .Include(c => c.HotelFacilities.Select(ad => ad.Facility))
                .Include(c => c.HotelImageLinks)
                .Include(c => c.HotelLocations.Select(ad => ad.Location))
                .Include(c => c.HotelMapLinks.Select(ad => ad.MapLink))
                .Include(c => c.HotelReports)
                .Include(c => c.HotelRoomCategories)
                .Include(c => c.HotelRoomFacilities.Select(ad => ad.Facility))
                .Include(c => c.HotelRoomTypes.Select(ad => ad.RoomType))
                .Where(h => h.CityCode == cityCode)
                .ToListAsync();
        }
    }
}