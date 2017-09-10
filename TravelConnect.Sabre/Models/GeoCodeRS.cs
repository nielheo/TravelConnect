using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Sabre.Models
{

    public class GeoCodeRS
    {
        public Result[] Results { get; set; }
        public Link[] Links { get; set; }
    }

    public class Result
    {
        public Geocoders GeoCodeRS { get; set; }
    }

    public class Geocoders
    {
        public string status { get; set; }
        public Place[] Place { get; set; }
    }

    public class Place
    {
        public string confidenceFactor { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

}
