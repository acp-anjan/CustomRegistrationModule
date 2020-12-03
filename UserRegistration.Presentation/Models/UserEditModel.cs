using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistration.Presentation.Models
{
    public class UserEditModel
    {
        public Guid UserId { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is requried.")]
        public string UserName { get; set; }

        [Display(Name = "User Email")]
        [Required(ErrorMessage = "User Email is requried.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        public string Email { get; set; }
        [Display(Name = "First Name")]
        [MaxLength(256, ErrorMessage = "Maxlength upto 256")]
        [Required(ErrorMessage = "First Name is requried.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [MaxLength(256, ErrorMessage = "Maxlength upto 256")]
        [Required(ErrorMessage = "Last Name is requried.")]
        public string LastName { get; set; }
        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is requried.")]
        public int RoleId { get; set; }
    }
}
