using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Gta.DataModels
{
    public class Location
    {
        [Key]
        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public ICollection<HotelLocation> HotelLocations { get; set; }
    }
}
