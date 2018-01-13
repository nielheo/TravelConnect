﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;
using TravelConnect.Gta.DataServices;
using TravelConnect.Gta.Interfaces;

namespace TravelConnect.Gta.Services
{
    public partial class GeoService : BaseService, IGeoService
    {
        private IGeoRepository _GeoRepository;
        public GeoService(IGeoRepository _GeoRepository)
        {
            this._GeoRepository = _GeoRepository;
        }


        public Task<List<City>> GetCities(string CountryCode, bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<City> GetCity(string Code, bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }
        
    }
}