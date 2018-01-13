using System.ComponentModel.DataAnnotations;

namespace TravelConnect.Gta.DataModels
{
    public class City
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(2)]
        public string CountryCode { get; set; }

        public Country Country { get; set; }
    }
}