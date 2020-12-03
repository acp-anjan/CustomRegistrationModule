using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserRegistration.Data.Models
{
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleId { get; set; }
        [MaxLength(32)]
        [Required]
        public string RoleName { get; set; }
        public virtual ICollection<Users> Users { get; set; }

    }
}
