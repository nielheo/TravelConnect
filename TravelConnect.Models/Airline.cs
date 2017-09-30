using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelConnect.Models
{
    public class Airline
    {
        [Required]
        [StringLength(2)]
        //Airline Code
        public string Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}
