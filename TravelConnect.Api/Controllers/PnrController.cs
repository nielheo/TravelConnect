using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.CreatePnr;
using TravelConnect.Models.Responses;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/Pnr")]
    [EnableCors("CorsPolicy")]
    public class PnrController : Controller
    {
        private IPnrService _PnrService;

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