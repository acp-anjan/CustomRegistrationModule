using System;
using System.Collections.Generic;
using System.Text;
using UserRegistration.Application.IRepository;
using UserRegistration.Data;

namespace UserRegistration.Application.Repository
{
    public class RepositoryWrapper : IRepowrapper
    {
        private UserContext _repoContext;
        public RepositoryWrapper(UserContext uContext)
        {
            _repoContext = uContext;
        }
        private IUserRepository _user;
        private IRoleRepository _role;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_repoContext);
                }
                return _role;
            }
        }
    }
}
