using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Models
{
    [NotMapped]
    public class FlightNumber
    {
        public string Airline { get; set; }
        public string Number { get; set; }
    }
}
