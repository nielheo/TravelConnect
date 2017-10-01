using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelConnect.Interfaces;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/airlines/{id}")]
    [RequestSizeLimit(100000000)]
    public class AirlinesController : Controller
    {
        private IFlightService _FlightService;

        public AirlinesController(IFlightService _FlightService)
        {
            this._FlightService = _FlightService;
        }

        [HttpGet]
        public async Task<Models.Responses.AirlineRS> Get(string id)
        {
            return await _FlightService.AirlineByCodeAsync(id);
        }
    }
}