using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Gta.DataModels
{
    public class HotelImageLink
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string HotelCode { get; set; }

        [ForeignKey("HotelCode")]
        public Hotel Hotel { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Caption { get; set; }

        [Required]
        [StringLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string Thumbnail { get; set; }

        [Required]
        [StringLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string Image { get; set; }
    }
}
