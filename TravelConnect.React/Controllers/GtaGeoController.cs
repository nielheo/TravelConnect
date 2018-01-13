using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelConnect.Gta.DataModels;
using TravelConnect.Gta.Interfaces;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/GtaGeo")]
    public class GtaGeoController : Controller
    {
        IGeoService _GeoService;

        public GtaGeoController(IGeoService _GeoService)
        {
            this._GeoService = _GeoService;
        }

        [Route("countries")]
        public async Task<List<Country>> Countries()
        {
            return await _GeoService.GetCountries(false);
        }
    }
}