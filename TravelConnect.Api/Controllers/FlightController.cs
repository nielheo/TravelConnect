using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelConnect.Interfaces;
using TravelConnect.Services.Models;
using TravelConnect.Models.Responses;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelConnect.Api.Controllers
{
    [Route("api/flight")]
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
        public async Task<AirLowFareSearchRS> Post([FromBody]SearchRQ request)
        {
            //SearchRQ rq = new SearchRQ
            //{
            //    Segments = new List<SegmentRQ>()
            //    {
            //        new SegmentRQ
            //        {
            //            Origin = "BKK",
            //            Destination = "SIN",
            //            Departure =DateTime.Today.AddDays(7)
            //        },
            //        new SegmentRQ
            //        {
            //            Origin = "SIN",
            //            Destination = "BKK",
            //            Departure =DateTime.Today.AddDays(10)
            //        }
            //    },
            //    Ptcs = new List<PtcRQ>
            //    {
            //        new PtcRQ
            //        {
            //            Code = "ADT",
            //            Quantity = 1
            //        },
            //        new PtcRQ
            //        {
            //            Code = "CNN",
            //            Quantity = 1
            //        }
            //    },
            //    AvailableFlightsOnly = true
            //};

            //string st = JsonConvert.SerializeObject(rq);
            //SearchRQ request = JsonConvert.DeserializeObject<SearchRQ>(rq);

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
