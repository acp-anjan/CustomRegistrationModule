using System;
using System.Collections.Generic;
using System.Text;

namespace UserRegistration.Application.IRepository
{
    public interface IRepowrapper
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
    }
}
