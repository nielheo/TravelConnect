using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelConnect.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string LoginName { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        public int UserLevel { get; set; }
        public int? ParentUserId { get; set; }
        public decimal Markup { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }

        [ForeignKey("ParentUserId")]
        public User ParentUser { get; set; }

        public ICollection<User> ChildUsers { get; set; }
    }
}
