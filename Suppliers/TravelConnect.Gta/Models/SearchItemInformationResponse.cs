using System;
using System.Collections.Generic;
using System.Text;

namespace TravelConnect.Gta.Models
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("Response", Namespace = "", IsNullable = false)]
    public partial class SearchItemInformationResponse
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
    public partial class ResponseResponseDetails
    {

        private ResponseResponseDetailsSearchItemInformationResponse searchItemInformationResponseField;

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponse SearchItemInformationResponse
        {
            get
            {
                return this.searchItemInformationResponseField;
            }
            set
            {
                this.searchItemInformationResponseField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponse
    {

        private ResponseResponseDetailsSearchItemInformationResponseItemDetails itemDetailsField;

        private string itemTypeField;

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetails ItemDetails
        {
            get
            {
                return this.itemDetailsField;
            }
            set
            {
                this.itemDetailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemType
        {
            get
            {
                return this.itemTypeField;
            }
            set
            {
                this.itemTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetails
    {

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetail itemDetailField;

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetail ItemDetail
        {
            get
            {
                return this.itemDetailField;
            }
            set
            {
                this.itemDetailField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetail
    {

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailCity cityField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailItem itemField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailLocationDetails locationDetailsField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformation hotelInformationField;

        private string copyrightField;

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailCity City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailItem Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailLocationDetails LocationDetails
        {
            get
            {
                return this.locationDetailsField;
            }
            set
            {
                this.locationDetailsField = value;
            }
        }

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformation HotelInformation
        {
            get
            {
                return this.hotelInformationField;
            }
            set
            {
                this.hotelInformationField = value;
            }
        }

        /// <remarks/>
        public string Copyright
        {
            get
            {
                return this.copyrightField;
            }
            set
            {
                this.copyrightField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailCity
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailItem
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailLocationDetails
    {

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailLocationDetailsLocation locationField;

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailLocationDetailsLocation Location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailLocationDetailsLocation
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformation
    {

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationAddressLines addressLinesField;

        private byte starRatingField;

        private string categoryField;

        private string[] areaDetailsField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationReport[] reportsField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationRoomCategory[] roomCategoriesField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationRoomTypes roomTypesField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationFacility[] roomFacilitiesField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationFacility1[] facilitiesField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationLinks linksField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationGeoCodes geoCodesField;

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationAddressLines AddressLines
        {
            get
            {
                return this.addressLinesField;
            }
            set
            {
                this.addressLinesField = value;
            }
        }

        /// <remarks/>
        public byte StarRating
        {
            get
            {
                return this.starRatingField;
            }
            set
            {
                this.starRatingField = value;
            }
        }

        /// <remarks/>
        public string Category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("AreaDetail", IsNullable = false)]
        public string[] AreaDetails
        {
            get
            {
                return this.areaDetailsField;
            }
            set
            {
                this.areaDetailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Report", IsNullable = false)]
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationReport[] Reports
        {
            get
            {
                return this.reportsField;
            }
            set
            {
                this.reportsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("RoomCategory", IsNullable = false)]
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationRoomCategory[] RoomCategories
        {
            get
            {
                return this.roomCategoriesField;
            }
            set
            {
                this.roomCategoriesField = value;
            }
        }

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationRoomTypes RoomTypes
        {
            get
            {
                return this.roomTypesField;
            }
            set
            {
                this.roomTypesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Facility", IsNullable = false)]
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationFacility[] RoomFacilities
        {
            get
            {
                return this.roomFacilitiesField;
            }
            set
            {
                this.roomFacilitiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Facility", IsNullable = false)]
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationFacility1[] Facilities
        {
            get
            {
                return this.facilitiesField;
            }
            set
            {
                this.facilitiesField = value;
            }
        }

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationLinks Links
        {
            get
            {
                return this.linksField;
            }
            set
            {
                this.linksField = value;
            }
        }

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationGeoCodes GeoCodes
        {
            get
            {
                return this.geoCodesField;
            }
            set
            {
                this.geoCodesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationAddressLines
    {

        private string addressLine1Field;

        private string addressLine2Field;

        private string addressLine3Field;

        private string addressLine4Field;

        private string telephoneField;

        private string faxField;

        private string emailAddressField;

        private string webSiteField;

        /// <remarks/>
        public string AddressLine1
        {
            get
            {
                return this.addressLine1Field;
            }
            set
            {
                this.addressLine1Field = value;
            }
        }

        /// <remarks/>
        public string AddressLine2
        {
            get
            {
                return this.addressLine2Field;
            }
            set
            {
                this.addressLine2Field = value;
            }
        }

        /// <remarks/>
        public string AddressLine3
        {
            get
            {
                return this.addressLine3Field;
            }
            set
            {
                this.addressLine3Field = value;
            }
        }

        /// <remarks/>
        public string AddressLine4
        {
            get
            {
                return this.addressLine4Field;
            }
            set
            {
                this.addressLine4Field = value;
            }
        }

        /// <remarks/>
        public string Telephone
        {
            get
            {
                return this.telephoneField;
            }
            set
            {
                this.telephoneField = value;
            }
        }

        /// <remarks/>
        public string Fax
        {
            get
            {
                return this.faxField;
            }
            set
            {
                this.faxField = value;
            }
        }

        /// <remarks/>
        public string EmailAddress
        {
            get
            {
                return this.emailAddressField;
            }
            set
            {
                this.emailAddressField = value;
            }
        }

        /// <remarks/>
        public string WebSite
        {
            get
            {
                return this.webSiteField;
            }
            set
            {
                this.webSiteField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationReport
    {

        private string typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationRoomCategory
    {

        private string descriptionField;

        private string roomDescriptionField;

        private string idField;

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string RoomDescription
        {
            get
            {
                return this.roomDescriptionField;
            }
            set
            {
                this.roomDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationRoomTypes
    {

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationRoomTypesRoomType[] roomTypeField;

        private byte roomCountField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RoomType")]
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationRoomTypesRoomType[] RoomType
        {
            get
            {
                return this.roomTypeField;
            }
            set
            {
                this.roomTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte RoomCount
        {
            get
            {
                return this.roomCountField;
            }
            set
            {
                this.roomCountField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationRoomTypesRoomType
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationFacility
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationFacility1
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationLinks
    {

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationLinksMapLinks mapLinksField;

        private ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationLinksImageLink[] imageLinksField;

        /// <remarks/>
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationLinksMapLinks MapLinks
        {
            get
            {
                return this.mapLinksField;
            }
            set
            {
                this.mapLinksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ImageLink", IsNullable = false)]
        public ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationLinksImageLink[] ImageLinks
        {
            get
            {
                return this.imageLinksField;
            }
            set
            {
                this.imageLinksField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationLinksMapLinks
    {

        private string mapPageLinkField;

        /// <remarks/>
        public string MapPageLink
        {
            get
            {
                return this.mapPageLinkField;
            }
            set
            {
                this.mapPageLinkField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationLinksImageLink
    {

        private string textField;

        private string thumbNailField;

        private string imageField;

        private ushort heightField;

        private ushort widthField;

        /// <remarks/>
        public string Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        public string ThumbNail
        {
            get
            {
                return this.thumbNailField;
            }
            set
            {
                this.thumbNailField = value;
            }
        }

        /// <remarks/>
        public string Image
        {
            get
            {
                return this.imageField;
            }
            set
            {
                this.imageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchItemInformationResponseItemDetailsItemDetailHotelInformationGeoCodes
    {

        private decimal latitudeField;

        private decimal longitudeField;

        /// <remarks/>
        public decimal Latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }

        /// <remarks/>
        public decimal Longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }
    }
}
