namespace TravelConnect.Gta.Models
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("Response", Namespace = "", IsNullable = false)]
    public partial class SearchCityResponse
    {
        private ResponseResponseDetails responseDetailsField;

        private string responseReferenceField;

        /// <remarks/>
        public ResponseResponseDetails ResponseDetails
        {
            get
            {
                return this.responseDetailsField;
            }
            set
            {
                this.responseDetailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ResponseReference
        {
            get
            {
                return this.responseReferenceField;
            }
            set
            {
                this.responseReferenceField = value;
            }
        }
    }

    public partial class ResponseResponseDetails
    {
        private ResponseResponseDetailsSearchCityResponse searchCityResponseField;

        /// <remarks/>
        public ResponseResponseDetailsSearchCityResponse SearchCityResponse
        {
            get
            {
                return this.searchCityResponseField;
            }
            set
            {
                this.searchCityResponseField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchCityResponse
    {
        private ResponseResponseDetailsSearchCityResponseCity[] cityDetailsField;

        private string countryCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("City", IsNullable = false)]
        public ResponseResponseDetailsSearchCityResponseCity[] CityDetails
        {
            get
            {
                return this.cityDetailsField;
            }
            set
            {
                this.cityDetailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchCityResponseCity
    {
        private string codeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}