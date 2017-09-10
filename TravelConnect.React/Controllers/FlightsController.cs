using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/flights")]
    public class FlightsController : Controller
    {
        private IFlightService _FlightService;

        public FlightsController(IFlightService _FlightService)
        {
            this._FlightService = _FlightService;
        }

        [HttpPost]
        public async Task<FlightSearchRS> Post([FromBody]FlightSearchRQ request)
        {
            return await _FlightService.AirLowFareSearchAsync(request);
        }
    }
}