using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Gta.DataModels
{
    public class HotelLocation
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
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string LocationCode { get; set; }

        [ForeignKey("LocationCode")]
        public Location Location { get; set; }
    }
}
