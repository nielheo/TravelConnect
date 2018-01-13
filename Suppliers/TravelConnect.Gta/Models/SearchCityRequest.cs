using System.Xml.Serialization;

namespace TravelConnect.Gta.Models
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("Request", Namespace = "", IsNullable = false)]
    public class SearchCityRequest : BaseRequest
    {
    }

    public partial class Requestdetails
    {
        public Searchcityrequest SearchCityRequest { get; set; }
    }

    public class Searchcityrequest
    {
        [XmlAttribute]
        public string CountryCode { get; set; }

        public string CityName { get; set; }
    }
}