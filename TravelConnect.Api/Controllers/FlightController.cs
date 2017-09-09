using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Interfaces.Models;
using TravelConnect.Models.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelConnect.Api.Controllers
{
    [Route("api/flight")]
    [DisableCors]
    public class FlightController : Controller
    {
        private IFlightService _FlightService;

        public FlightController(IFlightService _FlightService)
        {
            this._FlightService = _FlightService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<SearchRS> Post([FromBody]SearchRQ request)
        {
            

            return await _FlightService.AirLowFareSearch(request);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}