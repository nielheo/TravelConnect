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
        private IAirService _AirService;

        public FlightsController(IAirService _AirService)
        {
            this._AirService = _AirService;
        }

        [HttpPost]
        [Route("api/flights")]
        public async Task<FlightSearchRS> Post([FromBody]FlightSearchRQ request)
        {
            return await _AirService.AirLowFareSearchAsync(request);
        }
    }
}