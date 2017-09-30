using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelConnect.Models
{
    public class Airport
    {
        [Required]
        [StringLength(2)]
        //Airline Code
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string CityName { get; set; }

        [Required]
        [StringLength(50)]
        public string CountryName { get; set; }
    }
}
