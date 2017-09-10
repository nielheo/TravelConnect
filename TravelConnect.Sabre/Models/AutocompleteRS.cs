using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Sabre.Models
{
    public class AutocompleteRS
    {
        public Response Response { get; set; }
        public Link[] Links { get; set; }
    }

    public class Response
    {
        public Responseheader responseHeader { get; set; }
        public Grouped grouped { get; set; }
    }

    public class Responseheader
    {
        public int status { get; set; }
        public int QTime { get; set; }
    }

    public class Grouped
    {
        public Categoryair categoryAIR { get; set; }
    }

    public class Categoryair
    {
        public int matches { get; set; }
        public Doclist doclist { get; set; }
    }

    public class Doclist
    {
        public int numFound { get; set; }
        public int start { get; set; }
        public Doc[] docs { get; set; }
    }

    public class Doc
    {
        public string name { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string countryName { get; set; }
        public string category { get; set; }
        public string id { get; set; }
        public string dataset { get; set; }
        public string datasource { get; set; }
        public string confidenceFactor { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string iataCityCode { get; set; }
        public int ranking { get; set; }
        public string stateName { get; set; }
        public string state { get; set; }
    }
}
