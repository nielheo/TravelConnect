using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Gta.DataModels
{
    public class Hotel
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
        
        [StringLength(100)]
        [Column(TypeName = "float")]
        public float? Latitude { get; set; }

        [StringLength(100)]
        [Column(TypeName = "float")]
        public float? Longitude { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Copyright { get; set; }

        public ICollection<HotelLocation> HotelLocations { get; set; }
        public ICollection<HotelAreaDetail> HotelAreaDetails { get; set; }
        public ICollection<HotelReport> HotelReports { get; set; }
        public ICollection<HotelRoomCategory> HotelRoomCategories { get; set; }
        public ICollection<HotelRoomType> HotelRoomTypes { get; set; }
        public ICollection<HotelFacility> HotelFacilities { get; set; }
        public ICollection<HotelRoomFacility> HotelRoomFacilities { get; set; }
        public ICollection<HotelMapLink> HotelMapLinks { get; set; }
        public ICollection<HotelImageLink> HotelImageLinks { get; set; }
    }
}
