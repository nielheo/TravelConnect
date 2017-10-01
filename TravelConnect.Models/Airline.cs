using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Models
{
    [Table("Airline")]
    public class Airline
    {
        [Required]
        [StringLength(2)]
        //Airline Code
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }
    }
}