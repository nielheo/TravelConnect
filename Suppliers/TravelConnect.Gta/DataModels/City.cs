using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Gta.DataModels
{
    public class City
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

        [Required]
        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string CountryCode { get; set; }

        public Country Country { get; set; }

        public ICollection<Hotel> Hotels { get; set; }
    }
}