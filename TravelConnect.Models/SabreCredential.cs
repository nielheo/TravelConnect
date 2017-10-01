using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Models
{
    [Table("SabreCredential")]
    public class SabreCredential
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ClientId { get; set; }

        [Required]
        [StringLength(20)]
        public string ClientSecret { get; set; }

        [StringLength(300)]
        public string AccessToken { get; set; }

        public DateTime? ExpiryTime { get; set; }

        public bool IsActive { get; set; }
    }
}
