using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistration.Presentation.Models
{
    public class LoginViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is requried.")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is requried.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
