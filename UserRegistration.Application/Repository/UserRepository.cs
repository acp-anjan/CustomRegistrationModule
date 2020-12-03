using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UserRegistration.Application.IRepository;
using UserRegistration.Data;
using UserRegistration.Data.Models;
using UserRegistration.Application.Models;

namespace UserRegistration.Application.Repository
{
    public class UserRepository: RepositoryBase<Users>, IUserRepository
    {
        private UserContext _repoContext;
        public UserRepository(UserContext uContext): base(uContext)
        {
            _repoContext = uContext;
        }

        public string GetUserRole(int roleId)
        {
            var result = from objUser in _repoContext.Users
                         join objRole in _repoContext.Roles on objUser.RoleId equals objRole.RoleId
                         where objUser.RoleId == roleId
                         select objRole.RoleName;
            return result.FirstOrDefault();
        }

        public IEnumerable<UserBusinessModel> GetAllUsers()
        {
            var result = (from objUser in _repoContext.Users
                         join objRole in _repoContext.Roles on objUser.RoleId equals objRole.RoleId
                         select new UserBusinessModel{ 
                             UserId = objUser.UserId,
                             UserName = objUser.UserName,
                             FirstName = objUser.FirstName,
                             LastName = objUser.LastName,
                             Email = objUser.Email,
                             CreatedBy = objUser.CreatedBy,
                             CreatedDate = objUser.CreatedDate,
                             Role = objRole.RoleName
                         }).ToList();
            return result;

        }
    }
}
