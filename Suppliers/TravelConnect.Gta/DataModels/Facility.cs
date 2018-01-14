using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Gta.DataModels
{
    public class Facility
    {
        [Key]
        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public ICollection<HotelFacility> HotelFacilities { get; set; }
        public ICollection<HotelRoomFacility> HotelRoomFacilities { get; set; }
    }
}
