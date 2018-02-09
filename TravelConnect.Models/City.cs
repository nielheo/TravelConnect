using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Models
{
    [Table("Cities")]
    public class City
    {
        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string NameLong { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Permalink { get; set; }

        [Required]
        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string CountryCode { get; set; }

        [ForeignKey("CountryCode")]
        public Country Country { get; set; }
    }
}
