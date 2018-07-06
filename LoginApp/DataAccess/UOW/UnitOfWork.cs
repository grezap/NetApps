using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using DataAccess.Repository;

namespace DataAccess.UOW
{
    public class UnitOfWork : IUnitOfWork//, IDisposable
    {

        private readonly LoginAppDbContext _context;
        private bool disposed = false;

        private IGenericRepository<AppUser> _appUserRepository;

        private IGenericRepository<AppRole> _appRoleRepository;

        private IGenericRepository<AppUserToRole> _appUserToRoleRepository;

        public UnitOfWork(LoginAppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<AppUser> AppUserRepository
        {
            get { return _appUserRepository ?? (_appUserRepository = new GenericRepository<AppUser>(_context)); }
        }

        public IGenericRepository<AppRole> AppRoleRepository
        {
            get { return _appRoleRepository ?? (_appRoleRepository = new GenericRepository<AppRole>(_context)); }
        }

        public IGenericRepository<AppUserToRole> AppUserToRoleRepository
        {
            get { return _appUserToRoleRepository ?? (_appUserToRoleRepository = new GenericRepository<AppUserToRole>(_context)); }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
