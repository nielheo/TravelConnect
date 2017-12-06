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

                        Origin = "HKG",
                        Destination = "DXB",
                        Group = 0,
                        Key = "znpOmA8Q2BKAzonCAAAAAA==",
                        ClassOfService = "U",
                        FlightNumber = new Models.FlightNumber
                        {
                            Airline = "EK",
                            Number = "387"
                        },
                        DepartureTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,13,7,50,0),
                            GmtOffset = 8
                        },
                        ArrivalTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,13,11,50,0),
                            GmtOffset = 4
                        },
                        IsConnection = true,
                    },
                    new AirPriceSegment
                    {
                        Origin = "DXB",
                        Destination = "DEL",
                        Group = 0,
                        Key = "znpOmA8Q2BKA1onCAAAAAA==",
                        ClassOfService = "U",
                        FlightNumber = new Models.FlightNumber
                        {
                            Airline = "EK",
                            Number = "514"
                        },
                        DepartureTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,13,15,25,0),
                            GmtOffset = 4
                        },
                        ArrivalTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,13,20,20,0),
                            GmtOffset = (float)5.5
                        },
                        IsConnection = false
                    },

                    new AirPriceSegment
                    {

                        Origin = "DEL",
                        Destination = "DXB",
                        Group = 1,
                        Key = "znpOmA8Q2BKA7onCAAAAAA==",
                        ClassOfService = "U",
                        FlightNumber = new Models.FlightNumber
                        {
                            Airline = "EK",
                            Number = "511"
                        },
                        DepartureTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,18,11,00,0),
                            GmtOffset = (float)5.5
                        },
                        ArrivalTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,18,13,0,0),
                            GmtOffset = 4
                        },
                        IsConnection = true,
                    },
                    new AirPriceSegment
                    {
                        Origin = "DXB",
                        Destination = "HKG",
                        Group = 1,
                        Key = "znpOmA8Q2BKA9onCAAAAAA==",
                        ClassOfService = "U",
                        FlightNumber = new Models.FlightNumber
                        {
                            Airline = "EK",
                            Number = "386"
                        },
                        DepartureTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,18,18,25,0),
                            GmtOffset = 4
                        },
                        ArrivalTime = new Models.Timing {
                            Time = new System.DateTime(2018,6,19,6,20,0),
                            GmtOffset = (float)8
                        },
                        IsConnection = false
                    },
                },
                 CabinClass = "Economy",
                 Ptcs = new List<AirPricePtc>
                 {
                     new AirPricePtc { Code = "ADT" },
                     new AirPricePtc { Code = "CNN", Age = 8 },
                     new AirPricePtc { Code = "CHD", Age = 15 },
                     new AirPricePtc {Code = "INF", Age = 1}
                 }
            };

            var rs = await _AirService.AirPriceAsync(rq);
            return rs;
        }
    }
}