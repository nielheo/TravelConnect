using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Interfaces.Models;
using TravelConnect.Models.Responses;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/Flight")]
    public class FlightController : Controller
    {
        private IFlightService _FlightService;

        public FlightController(IFlightService _FlightService)
        {
            this._FlightService = _FlightService;
        }

        [HttpPost]
        public async Task<SearchRS> Post([FromBody]SearchRQ request)
        {
            return await _FlightService.AirLowFareSearch(request);
        }
    }
}