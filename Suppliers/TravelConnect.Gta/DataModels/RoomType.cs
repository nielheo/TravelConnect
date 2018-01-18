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
        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string Code { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; }
    }
}
