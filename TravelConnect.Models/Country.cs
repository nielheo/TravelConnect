using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Models
{
    [Table("Country")]
    public class Country
    {
        [Required]
        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Permalink { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public ICollection<Airport> Airports { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}