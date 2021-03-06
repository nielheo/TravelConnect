﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Gta.DataModels
{
    public class Country
    {
        [Key]
        [Required]
        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}