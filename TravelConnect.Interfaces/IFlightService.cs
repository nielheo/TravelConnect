using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Models.Responses;
using TravelConnect.Services.Models;

namespace TravelConnect.Interfaces
{
    public interface IFlightService
    {
        Task<AirLowFareSearchRS> AirLowFareSearch(SearchRQ request);
    }
}
