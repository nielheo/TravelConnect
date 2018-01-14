using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Gta.DataModels
{
    public class HotelRoomCategory
    {
        [Key]
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Id { get; set; }

        [Required]
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string HotelCode { get; set; }

        [ForeignKey("HotelCode")]
        public Hotel Hotel { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Required]
        [StringLength(4000)]
        [Column(TypeName = "varchar(4000)")]
        public string Description { get; set; }
    }
}
