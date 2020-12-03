using System;
using System.Collections.Generic;
using System.Text;

namespace UserRegistration.Application.Models
{
    public class UserBusinessModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; }

    }
}
