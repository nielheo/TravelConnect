using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Sabre.Models
{
    public class GeoCodeRQ
    {
        public Class1[] Request { get; set; }
    }

    public class Class1
    {
        public Geocoderq GeoCodeRQ { get; set; }
    }

    public class Geocoderq
    {
        public Placebyid PlaceById { get; set; }
    }

    public class Placebyid
    {
        public string Id { get; set; }
        public Browsecategory BrowseCategory { get; set; }
    }

    public class Browsecategory
    {
        public string name { get; set; }
    }


}
