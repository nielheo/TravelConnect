using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelConnect.Interfaces;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/airlines/{id}")]
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
            return await _FlightService.GetAirlineAsync(id);
        }
    }
}