using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TravelConnect.Gta.Models
{
    [XmlType(TypeName = "Request")]
    public class BaseRequest
    {
        public Source Source { get; set; }
        public Requestdetails RequestDetails { get; set; }
    }

    public class Source
    {
        public Requestorid RequestorID { get; set; }
        public Requestorpreferences RequestorPreferences { get; set; }
    }

    public class Requestorid
    {
        [XmlAttribute]
        public string Client { get; set; }

        [XmlAttribute]
        public string EMailAddress { get; set; }

        [XmlAttribute]
        public string Password { get; set; }
    }

    public class Requestorpreferences
    {
        public string RequestMode { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
    }

}
