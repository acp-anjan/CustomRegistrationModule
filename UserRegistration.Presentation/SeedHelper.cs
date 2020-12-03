using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Data;
using UserRegistration.Data.Models;
using UserRegistration.Presentation.Helper;

namespace UserRegistration.Presentation
{
    public class SeedHelper
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<UserContext>();
            context.Database.EnsureCreated();

            

            if (!context.Roles.Any())
            {
                //Password work
                var userSalt = CryptoService.GenerateSalt();
                byte[] bytePassword = Encoding.UTF8.GetBytes("@@Pa1234");
                var hmac = CryptoService.ComputeHMAC256(bytePassword, userSalt);
                var password = Convert.ToBase64String(hmac);
                var passwordSalt = Convert.ToBase64String(userSalt);

                //role and user work
                var roleAdmin = new Roles() {RoleId = 1, RoleName = "Admin" };
                var roleCustomer = new Roles() { RoleId = 2, RoleName = "Customer" };

                context.Add(roleAdmin);
                context.Add(roleCustomer);

                context.Users.Add(new Users()
                {
                    UserName = "SuperAdmin",
                    FirstName = "John",
                    LastName = "Cena",
                    CreatedBy = "SuperAdmin",
                    CreatedDate = DateTime.Now,
                    Email = "email@exampl.com",
                    Password = password,
                    PasswordSalt = passwordSalt,
                    RoleId = roleAdmin.RoleId
                });
                context.SaveChanges();
            }
        }
    }
}
