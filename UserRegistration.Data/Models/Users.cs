using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserRegistration.Data.Models
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }
        [MaxLength(256)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(256)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(32)]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
        [MaxLength(32)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public int RoleId { get; set; }
        public virtual Roles Role { get; set; }
    }
}
