using System.Collections.Generic;
using System.Linq;
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
            if (forceRefresh)
            {
                var couns = await RefreshCountry();
                return couns.FirstOrDefault(c => c.Code.ToUpper() == Code.ToUpper());
            }

            var countries = await _GeoRepository.GetCountry(Code.ToUpper());

            if (countries == null)
            {
                var cns = await RefreshCountry();
                return cns.FirstOrDefault(c => c.Code.ToUpper() == Code.ToUpper());
            }
            else
                return countries;
        }
    }
}