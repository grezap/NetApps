using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.UOW
{
    public interface IUnitOfWork
    {

        IGenericRepository<AppUser> AppUserRepository { get; }
        IGenericRepository<AppRole> AppRoleRepository { get; }
        IGenericRepository<AppUserToRole> AppUserToRoleRepository { get; }
        void Save();
    }
}
