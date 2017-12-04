using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.Interfaces
{
    public interface IAirService
    {
        Task<FlightSearchRS> AirLowFareSearchAsync(FlightSearchRQ request);
        Task<AirPriceRS> AirPriceAsync(AirPriceRQ request);
        Task<List<string>> GetTopDestinationsAsync(string airportCode);
    }
}
