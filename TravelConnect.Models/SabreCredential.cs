using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelConnect.Models
{
    public class SabreCredential
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ClientId { get; set; }

        [Required]
        [StringLength(20)]
        public string ClientSecret { get; set; }

        public bool IsActive { get; set; }
    }
}
