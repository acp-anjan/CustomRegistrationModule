using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistration.Presentation.Models
{
    public class UserCreateModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is requried.")]
        [MaxLength(32, ErrorMessage ="Maxlength upto 32")]
        [MinLength(5, ErrorMessage ="At least 5 chars")]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Remote(action:"UserNameExists", controller:"Account", ErrorMessage = "Username already exists.")]
        public string UserName { get; set; }

        [Display(Name = "User Email")]
        [Required(ErrorMessage = "User Email is requried.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        [Remote("EmailExists", "Account", ErrorMessage = "User Email already exists.")]
        public string Email { get; set; }
        [Display(Name = "First Name")]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [MaxLength(256, ErrorMessage = "Maxlength upto 256")]
        [Required(ErrorMessage = "First Name is requried.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [MaxLength(256, ErrorMessage = "Maxlength upto 256")]
        [Required(ErrorMessage = "Last Name is requried.")]
        public string LastName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is requried.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{8,}$", ErrorMessage = "The {0} should contains at least 8 characters, one uppercase, one lowercase, one digits and one unique characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is requried.")]
        public int RoleId { get; set; }

    }
}
