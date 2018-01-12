using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TravelConnect.Gta.Models
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("Response", Namespace = "", IsNullable = false)]
    public partial class SearchHotelPriceResponse
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetails
    {

        private ResponseResponseDetailsSearchHotelPriceResponse searchHotelPriceResponseField;

        private string languageField;

        /// <remarks/>
        public ResponseResponseDetailsSearchHotelPriceResponse SearchHotelPriceResponse
        {
            get
            {
                return this.searchHotelPriceResponseField;
            }
            set
            {
                this.searchHotelPriceResponseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponse
    {

        private ResponseResponseDetailsSearchHotelPriceResponseHotel[] hotelDetailsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Hotel", IsNullable = false)]
        public ResponseResponseDetailsSearchHotelPriceResponseHotel[] HotelDetails
        {
            get
            {
                return this.hotelDetailsField;
            }
            set
            {
                this.hotelDetailsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotel
    {

        private ResponseResponseDetailsSearchHotelPriceResponseHotelCity cityField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelItem itemField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelLocation[] locationDetailsField;

        private int starRatingField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelHotelRooms hotelRoomsField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategory[] roomCategoriesField;

        private bool hasExtraInfoField;

        private bool hasPicturesField;

        private bool hasPicturesFieldSpecified;

        private bool hasMapField;

        private bool hasMapFieldSpecified;

        /// <remarks/>
        public ResponseResponseDetailsSearchHotelPriceResponseHotelCity City
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
        public ResponseResponseDetailsSearchHotelPriceResponseHotelItem Item
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
        [System.Xml.Serialization.XmlArrayItemAttribute("Location", IsNullable = false)]
        public ResponseResponseDetailsSearchHotelPriceResponseHotelLocation[] LocationDetails
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
        public int StarRating
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
        public ResponseResponseDetailsSearchHotelPriceResponseHotelHotelRooms HotelRooms
        {
            get
            {
                return this.hotelRoomsField;
            }
            set
            {
                this.hotelRoomsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("RoomCategory", IsNullable = false)]
        public ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategory[] RoomCategories
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool HasExtraInfo
        {
            get
            {
                return this.hasExtraInfoField;
            }
            set
            {
                this.hasExtraInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool HasPictures
        {
            get
            {
                return this.hasPicturesField;
            }
            set
            {
                this.hasPicturesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool HasPicturesSpecified
        {
            get
            {
                return this.hasPicturesFieldSpecified;
            }
            set
            {
                this.hasPicturesFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool HasMap
        {
            get
            {
                return this.hasMapField;
            }
            set
            {
                this.hasMapField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool HasMapSpecified
        {
            get
            {
                return this.hasMapFieldSpecified;
            }
            set
            {
                this.hasMapFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelCity
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelItem
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelLocation
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelHotelRooms
    {

        private ResponseResponseDetailsSearchHotelPriceResponseHotelHotelRoomsHotelRoom hotelRoomField;

        /// <remarks/>
        public ResponseResponseDetailsSearchHotelPriceResponseHotelHotelRoomsHotelRoom HotelRoom
        {
            get
            {
                return this.hotelRoomField;
            }
            set
            {
                this.hotelRoomField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelHotelRoomsHotelRoom
    {

        private string codeField;

        private byte numberOfRoomsField;

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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte NumberOfRooms
        {
            get
            {
                return this.numberOfRoomsField;
            }
            set
            {
                this.numberOfRoomsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategory
    {

        private string descriptionField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryItemPrice itemPriceField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryOffer offerField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryConfirmation confirmationField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryMeals mealsField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryInformation[] essentialInformationField;

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
        public ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryItemPrice ItemPrice
        {
            get
            {
                return this.itemPriceField;
            }
            set
            {
                this.itemPriceField = value;
            }
        }

        /// <remarks/>
        public ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryOffer Offer
        {
            get
            {
                return this.offerField;
            }
            set
            {
                this.offerField = value;
            }
        }

        /// <remarks/>
        public ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryConfirmation Confirmation
        {
            get
            {
                return this.confirmationField;
            }
            set
            {
                this.confirmationField = value;
            }
        }

        /// <remarks/>
        public ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryMeals Meals
        {
            get
            {
                return this.mealsField;
            }
            set
            {
                this.mealsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Information", IsNullable = false)]
        public ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryInformation[] EssentialInformation
        {
            get
            {
                return this.essentialInformationField;
            }
            set
            {
                this.essentialInformationField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryItemPrice
    {

        private string currencyField;

        private decimal grossWithoutDiscountField;

        private bool grossWithoutDiscountFieldSpecified;

        private decimal includedOfferDiscountField;

        private bool includedOfferDiscountFieldSpecified;

        private decimal valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Currency
        {
            get
            {
                return this.currencyField;
            }
            set
            {
                this.currencyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal GrossWithoutDiscount
        {
            get
            {
                return this.grossWithoutDiscountField;
            }
            set
            {
                this.grossWithoutDiscountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool GrossWithoutDiscountSpecified
        {
            get
            {
                return this.grossWithoutDiscountFieldSpecified;
            }
            set
            {
                this.grossWithoutDiscountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal IncludedOfferDiscount
        {
            get
            {
                return this.includedOfferDiscountField;
            }
            set
            {
                this.includedOfferDiscountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IncludedOfferDiscountSpecified
        {
            get
            {
                return this.includedOfferDiscountFieldSpecified;
            }
            set
            {
                this.includedOfferDiscountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryOffer
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryConfirmation
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryMeals
    {

        private ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryMealsBasis basisField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryMealsBreakfast breakfastField;

        /// <remarks/>
        public ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryMealsBasis Basis
        {
            get
            {
                return this.basisField;
            }
            set
            {
                this.basisField = value;
            }
        }

        /// <remarks/>
        public ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryMealsBreakfast Breakfast
        {
            get
            {
                return this.breakfastField;
            }
            set
            {
                this.breakfastField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryMealsBasis
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryMealsBreakfast
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryInformation
    {

        private string textField;

        private ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryInformationDateRange dateRangeField;

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
        public ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryInformationDateRange DateRange
        {
            get
            {
                return this.dateRangeField;
            }
            set
            {
                this.dateRangeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ResponseResponseDetailsSearchHotelPriceResponseHotelRoomCategoryInformationDateRange
    {

        private System.DateTime fromDateField;

        private System.DateTime toDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime FromDate
        {
            get
            {
                return this.fromDateField;
            }
            set
            {
                this.fromDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime ToDate
        {
            get
            {
                return this.toDateField;
            }
            set
            {
                this.toDateField = value;
            }
        }
    }
}
