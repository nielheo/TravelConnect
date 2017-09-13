using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [RequestSizeLimit(100000000)]
    public class FlightsController : Controller
    {
        private IFlightService _FlightService;

        public FlightsController(IFlightService _FlightService)
        {
            this._FlightService = _FlightService;
        }

        [HttpPost]
        [Route("api/flights")]
        public async Task<FlightSearchRS> Post([FromBody]FlightSearchRQ request)
        {
            return await _FlightService.AirLowFareSearchAsync(request);
        }

        [HttpGet]
        [Route("api/flights/{id}")]
        public async Task<FlightSearchRS> Get(string id, int page)
        {
            return await _FlightService.NextAirLowFareSearchAsync(id, page);
        }
    }
}