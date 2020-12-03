using System;
using System.Collections.Generic;
using System.Text;
using UserRegistration.Application.Models;
using UserRegistration.Data.Models;

namespace UserRegistration.Application.IRepository
{
    public interface IUserRepository: IRepositoryBase<Users>
    {
        string GetUserRole(int roleId);
        IEnumerable<UserBusinessModel> GetAllUsers();
    }
}
