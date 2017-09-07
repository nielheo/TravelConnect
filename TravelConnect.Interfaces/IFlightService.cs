using System;
using System.Collections.Generic;
using System.Text;
using TravelConnect.Models.Responses;
using TravelConnect.Services.Models;

namespace TravelConnect.Interfaces
{
    public interface IFlightService
    {
        AirLowFareSearchRS AirLowFareSearch(SearchRQ request);
    }
}
