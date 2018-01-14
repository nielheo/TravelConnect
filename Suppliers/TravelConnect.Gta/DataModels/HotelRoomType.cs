﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelConnect.Gta.DataModels
{
    public class HotelRoomType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string HotelCode { get; set; }

        [ForeignKey("HotelCode")]
        public Hotel Hotel { get; set; }

        [Required]
        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string RoomTypeCode { get; set; }

        [ForeignKey("HotelCode")]
        public RoomType RoomType { get; set; }
    }
}
