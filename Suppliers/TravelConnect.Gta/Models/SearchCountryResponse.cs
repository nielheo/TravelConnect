namespace TravelConnect.Gta.Models
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("Response", Namespace = "", IsNullable = false)]
    public partial class SearchCountryResponse
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ResponseResponseDetails
    {
        private ResponseResponseDetailsSearchCountryResponse searchCountryResponseField;

        /// <remarks/>
        public ResponseResponseDetailsSearchCountryResponse SearchCountryResponse
        {
            get
            {
                return this.searchCountryResponseField;
            }
            set
            {
                this.searchCountryResponseField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchCountryResponse
    {
        private ResponseResponseDetailsSearchCountryResponseCountry[] countryDetailsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Country", IsNullable = false)]
        public ResponseResponseDetailsSearchCountryResponseCountry[] CountryDetails
        {
            get
            {
                return this.countryDetailsField;
            }
            set
            {
                this.countryDetailsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchCountryResponseCountry
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