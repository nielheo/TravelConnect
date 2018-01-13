using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelConnect.Gta.DataModels
{
    public class Country
    {
        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}