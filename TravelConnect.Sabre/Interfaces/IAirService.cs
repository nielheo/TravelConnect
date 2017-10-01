using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Sabre.Interfaces
{
    public interface IAirService
    {
        Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request);
    }
}
