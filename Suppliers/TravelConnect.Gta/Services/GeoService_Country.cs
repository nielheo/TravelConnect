using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Gta.DataModels;
using TravelConnect.Gta.Interfaces;

namespace TravelConnect.Gta.Services
{
    public partial class GeoService : BaseService, IGeoService
    {
        private async Task<List<Country>> RefreshCountry()
        {
            var countries = await GetGtaCountries();
            _GeoRepository.InsertCountries(countries);

            return countries;
        }

        public async Task<List<Country>> GetCountries(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                return await RefreshCountry();
            }

            var countries = await _GeoRepository.GetCountries();

            if (countries.Count == 0)
                return await RefreshCountry();
            else
                return countries;
        }

        public async Task<Country> GetCountry(string Code, bool forceRefresh = false)
        {
            throw new System.NotImplementedException();
        }
    }
}
