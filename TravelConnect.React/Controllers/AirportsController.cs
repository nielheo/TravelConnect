using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Responses;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/airports/{id}")]
    public class AirportsController : Controller
    {
        private IGeoService _GeoService;

        public AirportsController(IGeoService _GeoService)
        {
            this._GeoService = _GeoService;
        }

        [HttpGet]
        public async Task<AirportRS> Get(string id)
        {
            return await _GeoService.GetAirtportByCodeAsync(id);
        }
    }
}