using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Gta.DataModels
{
    public class RoomType
    {
        [Key]
        [Required]
        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string Code { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
    }
}
