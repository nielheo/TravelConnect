using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelConnect.Models.Responses;
using TravelConnect.Interfaces;

namespace TravelConnect.Api.Controllers
{
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