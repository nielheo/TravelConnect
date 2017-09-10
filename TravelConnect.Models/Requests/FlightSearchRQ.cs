using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelConnect.Models.Requests
{
    public class SegmentRQ
    {
        public DateTime Departure { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

    public class PtcRQ
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
    }

    public class FlightSearchRQ
    {
        public List<SegmentRQ> Segments { get; set; }
        public List<PtcRQ> Ptcs { get; set; }
        public bool AvailableFlightsOnly { get; set; }
        public bool DirectFlightsOnly { get; set; }

        
    }
}
