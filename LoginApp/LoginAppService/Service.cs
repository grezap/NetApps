using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Model;
using DataAccess.UOW;
using Domain.Entities;
using LoginAppService.Mapping;
using Microsoft.Extensions.Logging;

namespace LoginAppService
{
    public class Service : IService
    {

        private IUnitOfWork _uow;
        private ILogger _log;

        public Service(IUnitOfWork uow, ILogger<Service> logger)
        {
            _uow = uow;
            _log = logger;
            AutoMapperConfiguration.Configure();
        }

        #region ApplicationUser

        public void DeleteUser(ApplicationUser applicationUser)
        {
            
            try
            {
                _uow.AppUserRepository.Delete(Mapper.Map<AppUser>(applicationUser));
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUsers(List<ApplicationUser> applicationUsers)
        {
            try
            {
                _uow.AppUserRepository.DeleteMany(Mapper.Map<List<AppUser>>(applicationUsers));
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApplicationUser GetUserById(int id)
        {
            _log.LogInformation("Called GetUserbyId");

            try
            {
                return Mapper.Map<ApplicationUser>(_uow.AppUserRepository.GetById(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long GetUserCount()
        {
            try
            {
                return _uow.AppUserRepository.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            try
            {
                return Mapper.Map<List<ApplicationUser>>(_uow.AppUserRepository.Get());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApplicationUser GetUserByUserName(string userName)
        {
            try
            {
                return Mapper.Map<ApplicationUser>(_uow.AppUserRepository.Query().Where(q => q.Username == userName).FirstOrDefault());                                           
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw ex;
            }
        }

        public ApplicationUser GetUserByNormalizedEmail(string normalizedEmail)
        {
            try
            {
                return Mapper.Map<ApplicationUser>(_uow.AppUserRepository.Query().Where(q => q.NormalizedEmail == normalizedEmail).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw ex;
            }
        }

        public ApplicationUser GetUserByNormalizedUserName(string normalizedUserName)
        {
            try
            {
                return Mapper.Map<ApplicationUser>(_uow.AppUserRepository.Query().Where(q => q.NormalizedUserName.ToUpper() == normalizedUserName.ToUpper()).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw ex;
            }
        }

        public List<ApplicationUser> GetUsersByRole(ApplicationRole role)
        {
            try
            {
                var userToRoles = _uow.AppUserToRoleRepository.Query().Where(ur => ur.RoleId == role.Id);
                var users = (from u in _uow.AppUserRepository.Query()
                             join ur in userToRoles on u.Id equals ur.UserId
                             select u).ToList();
                return Mapper.Map<List<ApplicationUser>>(users);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw ex;
            }
        }

        public void InsertUser(ApplicationUser applicationUser)
        {
            try
            {
                _uow.AppUserRepository.Insert(Mapper.Map<AppUser>(applicationUser));
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertUsers(List<ApplicationUser> applicationUsers)
        {
            try
            {
                _uow.AppUserRepository.InsertMany(Mapper.Map<List<AppUser>>(applicationUsers));
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateUser(ApplicationUser applicationUser)
        {
            try
            {
                _uow.AppUserRepository.Update(Mapper.Map<AppUser>(applicationUser));
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Application Role

        public void DeleteRole(ApplicationRole applicationRole)
        {
            try
            {
                _uow.AppRoleRepository.Delete(Mapper.Map<AppRole>(applicationRole));
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteRoles(List<ApplicationRole> applicationRoles)
        {
            try
            {
                _uow.AppRoleRepository.DeleteMany(Mapper.Map<List<AppRole>>(applicationRoles));
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApplicationRole GetApplicationRole(int id)
        {
            try
            {
                return Mapper.Map<ApplicationRole>(_uow.AppRoleRepository.GetById(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApplicationRole GetApplicationRoleByRoleName(string roleName)
        {
            try
            {
                return Mapper.Map<ApplicationRole>(_uow.AppRoleRepository.Query().Where(r=> r.Name == roleName).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw ex;
            }
        }

        public List<ApplicationRole> GetApplicationRoles()
        {
            try
            {
                return Mapper.Map<List<ApplicationRole>>(_uow.AppRoleRepository.Get());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApplicationRole GetApplicationRoleByNormalizedName(string normalizedName)
        {
            try
            {
                //var test = _uow.AppRoleRepository.Query().Where(r => r.NormalizedName.ToUpper() == normalizedName.ToUpper());
                return Mapper.Map<ApplicationRole>(_uow.AppRoleRepository.Query().Where(r=>r.NormalizedName.ToUpper() == normalizedName.ToUpper()).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw ex;
            }
        }

        public List<ApplicationRole> GetApplicationRolesByUser(ApplicationUser applicationUser)
        {
            try
            {
                var userToRoles = _uow.AppUserToRoleRepository.Query().Where(ur => ur.UserId == applicationUser.Id);
                var roles = (from r in _uow.AppRoleRepository.Query()
                             join ur in userToRoles on r.Id equals ur.RoleId
                             select r).ToList();
                return Mapper.Map<List<ApplicationRole>>(roles);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw ex;
            }
        }

        public void InsertRole(ApplicationRole applicationRole)
        {
            try
            {
                _uow.AppRoleRepository.Insert(Mapper.Map<AppRole>(applicationRole));
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertRoles(List<ApplicationRole> applicationRoles)
        {
            try
            {
                _uow.AppRoleRepository.InsertMany(Mapper.Map<List<AppRole>>(applicationRoles));
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRole(ApplicationRole applicationRole)
        {
            try
            {
                _uow.AppRoleRepository.Update(Mapper.Map<AppRole>(applicationRole));
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Application UserToRole

        public void InsertUserToRole(ApplicationUserRole applicationUserRole)
        {
            try
            {
                _uow.AppUserToRoleRepository.Insert(Mapper.Map<AppUserToRole>(applicationUserRole));
                _uow.Save();
            }
            catch ( Exception ex)
            {

                throw ex;
            }
        }

        public void RemoveUserFromRole(ApplicationUserRole applicationUserRole)
        {
            try
            {
                _uow.AppUserToRoleRepository.Delete(Mapper.Map<AppUserToRole>(applicationUserRole));
                _uow.Save();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ApplicationUserRole> GetApplicationUserRoles()
        {
            try
            {
                return Mapper.Map<List<ApplicationUserRole>>( _uow.AppUserToRoleRepository.Get());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ApplicationUserRole> GetApplicationUserRolesByUserId(int userId)
        {
            try
            {
                return Mapper.Map<List<ApplicationUserRole>>(_uow.AppUserToRoleRepository.Query().Where(ur=> ur.UserId == userId)).ToList();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw ex;
            }
        }

        public List<ApplicationUserRole> GetApplicationUserRolesByRoleId(int roleId)
        {
            try
            {
                return Mapper.Map<List<ApplicationUserRole>>(_uow.AppUserToRoleRepository.Query().Where(ur => ur.RoleId == roleId)).ToList();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                throw ex;
            }
        }

        public List<ApplicationUserRole> GetApplicationUserRolesByRoleIdAndUserId(int roleId, int userId)
        {
            try
            {
                return Mapper.Map<List<ApplicationUserRole>>(_uow.AppUserToRoleRepository.Query().Where(ur=>ur.UserId == userId && ur.RoleId == roleId)).ToList();
            }
            catch ( Exception ex)
            {
                _log.LogError(ex.Message);
                throw ex;
            }
        }


        #endregion

    }
}
