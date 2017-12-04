using System;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.uAPI.Services
{
    public partial class AirService : IAirService
    {
        public Task<AirPriceRS> AirPriceAsync(AirPriceRQ request)
        {
            throw new NotImplementedException();
        }
    }
}