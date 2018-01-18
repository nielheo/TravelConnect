using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Gta.DataModels
{
    public class HotelBase
    {
        [Key]
        [Required]
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string Code { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string CityCode { get; set; }
        public City City { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? StarRating { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Address1 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Address2 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Address3 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Address4 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Phone { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Fax { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Website { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Category { get; set; }

        //[Column(TypeName = "float")]
        public float? Latitude { get; set; }

        //[Column(TypeName = "float")]
        public float? Longitude { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Copyright { get; set; }

        public HotelBase() { }

        public HotelBase(HotelBase toCopy)
        {
            var baseObject = (HotelBase)toCopy;
            foreach(var f in baseObject.GetType().GetProperties())
            {
                if (this.GetType().GetProperty(f.Name) != null)
                    f.SetValue(this, f.GetValue(toCopy));
            }
        }
    }
}
