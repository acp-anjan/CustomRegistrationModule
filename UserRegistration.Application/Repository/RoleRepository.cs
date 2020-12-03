using System;
using System.Collections.Generic;
using System.Text;
using UserRegistration.Application.IRepository;
using UserRegistration.Data;
using UserRegistration.Data.Models;

namespace UserRegistration.Application.Repository
{
    public class RoleRepository: RepositoryBase<Roles>, IRoleRepository
    {
        private UserContext _repoContext;
        public RoleRepository(UserContext uContext) : base(uContext)
        {
            _repoContext = uContext;
        }
    }
}
