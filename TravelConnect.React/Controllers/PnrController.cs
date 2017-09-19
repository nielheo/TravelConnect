using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelConnect.Models.Responses;
using TravelConnect.Models.CreatePnr;
using TravelConnect.Interfaces;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/Pnr")]
    public class PnrController : Controller
    {
        IPnrService _PnrService;

        public PnrController(IPnrService _PnrService)
        {
            this._PnrService = _PnrService;
        }

        [HttpPost]
        public async Task<CreatePnrRS> PostAsync([FromBody]CreatePnrRQ request)
        {
            Task<CreatePnrRS> response = _PnrService.CreatePnrAsync(request);
            
            return await response;
        }
    }
}