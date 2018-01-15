using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TravelConnect.Gta.Models
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("Request", Namespace = "", IsNullable = false)]
    public class SearchItemInformationRequest : BaseRequest
    {
    }

    public partial class Requestdetails
    {
        public Searchiteminformationrequest SearchItemInformationRequest { get; set; }
    }

    public class Searchiteminformationrequest
    {
        public Itemdestination ItemDestination { get; set; }
        public string ItemCode { get; set; }

        [XmlAttribute]
        public string ItemType { get; set; }
    }

    


}
