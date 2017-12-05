using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelConnect.Interfaces;
using TravelConnect.Models.Requests;
using TravelConnect.Models.Responses;

namespace TravelConnect.React.Controllers
{
    [Produces("application/json")]
    [Route("api/airprice")]
    [RequestSizeLimit(100000000)]
    public class AirPriceController : Controller
    {
        private IAirService _AirService;

        public AirPriceController(IAirService _AirService)
        {
            this._AirService = _AirService;
        }

        [HttpGet]
        public async Task<AirPriceRS> Get()
        {
            AirPriceRQ rq = new AirPriceRQ
            {
                Segments = new List<AirPriceSegment>
                {

                    new AirPriceSegment
                    {

                        Origin = "BKK",
                        Destination = "SIN",
                        Group = 0,
                        Key = "3GK2TA8Q2BKA2P8kAAAAAA==",
                        FlightNumber = new Models.FlightNumber
                        {
                            Airline = "CX",
                            Number = "713"
                        },
                        DepartureTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,13,11,50,0),
                            GmtOffset = 7
                        },
                        ArrivalTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,13,15,20,0),
                            GmtOffset = 8
                        },

                    },
                    new AirPriceSegment
                    {
                        Origin = "SIN",
                        Destination = "BKK",
                        Group = 1,
                        Key = "3GK2TA8Q2BKA4P8kAAAAAA==",
                        FlightNumber = new Models.FlightNumber
                        {
                            Airline = "CX",
                            Number = "712"
                        },
                        DepartureTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,18,13,35,0),
                            GmtOffset = 8
                        },
                        ArrivalTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,18,15,00,0),
                            GmtOffset = 7
                        },
                    }
                }
            };

            var rs = await _AirService.AirPriceAsync(rq);
            return rs;
        }
    }
}