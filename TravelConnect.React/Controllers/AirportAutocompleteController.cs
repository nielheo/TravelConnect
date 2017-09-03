using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelConnect.Interfaces;
using TravelConnect.Models.Responses;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/airportautocomplete")]
    public class AirportAutocompleteController : Controller
    {
        private IGeoService _GeoService;

        public AirportAutocompleteController(IGeoService _GeoService)
        {
            this._GeoService = _GeoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<AirportAutocompleteRS> Get(string query)
        {
            AirportAutocompleteRS response = await _GeoService.GetAirportAutocompleteAsync(query);

            return response;
        }
    }
}