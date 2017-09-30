using System;
using System.ComponentModel.DataAnnotations;

namespace TravelConnect.Models
{
    public class Airport
    {
        [Required]
        [StringLength(3)]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string CityName { get; set; }

        [Required]
        [StringLength(2)]
        public string CountryCode { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }
    }
}