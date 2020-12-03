using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistration.Presentation.Models
{
    public class CustomerRegistrationViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is requried.")]
        [Remote("UserNameExists", "Account", ErrorMessage = "Username already exists.")]
        public string UserName { get; set; }

        [Display(Name = "User Email")]
        [Required(ErrorMessage = "User Email is requried.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        [Remote("EmailExists", "Account", ErrorMessage = "User Email already exists.")]
        public string Email { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is requried.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is requried.")]
        public string LastName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is requried.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{8,}$", ErrorMessage = "The {0} does not meet requirements.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is requried.")]
        [Compare("Password", ErrorMessage = "Password & Confirm Password should be same.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
