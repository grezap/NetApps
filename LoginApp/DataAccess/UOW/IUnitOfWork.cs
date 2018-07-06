using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UOW
{
    public interface IUnitOfWork
    {

        IGenericRepository<AppUser> AppUserRepository { get; }
        IGenericRepository<AppRole> AppRoleRepository { get; }
        IGenericRepository<AppUserToRole> AppUserToRoleRepository { get; }
        void Save();
        Task<int> SaveAsync();
    }
}
