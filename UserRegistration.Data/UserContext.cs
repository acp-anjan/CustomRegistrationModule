using Microsoft.EntityFrameworkCore;
using System;
using UserRegistration.Data.Models;

namespace UserRegistration.Data
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
    }
}
