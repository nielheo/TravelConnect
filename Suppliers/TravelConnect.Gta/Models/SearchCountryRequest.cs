using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Gta.Models
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("Request", Namespace = "", IsNullable = false)]
    public class SearchCountryRequest : BaseRequest
    {

    }

    public partial class Requestdetails
    {
        public Searchcountryrequest SearchCountryRequest { get; set; }
    }

    public class Searchcountryrequest
    {
        public string CountryName { get; set; }
    }
}
