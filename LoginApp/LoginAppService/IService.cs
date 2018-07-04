using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginAppService
{
    public interface IService
    {
        #region Application Users

        Task<List<ApplicationUser>> GetUsers();
        ApplicationUser GetUserById(int id);
        ApplicationUser GetUserByUserName(string userName);
        ApplicationUser GetUserByNormalizedEmail(string normalizedEmail);
        ApplicationUser GetUserByNormalizedUserName(string normalizedUserName);
        List<ApplicationUser> GetUsersByRole(ApplicationRole role);
        void InsertUser(ApplicationUser applicationUser);
        void InsertUsers(List<ApplicationUser> applicationUsers);
        void DeleteUser(ApplicationUser applicationUser);
        void DeleteUsers(List<ApplicationUser> applicationUsers);
        void UpdateUser(ApplicationUser applicationUser);
        long GetUserCount();

        #endregion

        #region Application Roles

        List<ApplicationRole> GetApplicationRoles();
        ApplicationRole GetApplicationRole(int id);
        ApplicationRole GetApplicationRoleByRoleName(string roleName);
        ApplicationRole GetApplicationRoleByNormalizedName(string normalizedName);
        List<ApplicationRole> GetApplicationRolesByUser(ApplicationUser applicationUser);
        void InsertRole(ApplicationRole applicationRole);
        void InsertRoles(List<ApplicationRole> applicationRoles);
        void DeleteRole(ApplicationRole applicationRole);
        void DeleteRoles(List<ApplicationRole> applicationRoles);
        void UpdateRole(ApplicationRole applicationRole);

        #endregion

        #region ApplicationUserToRole

        void InsertUserToRole(ApplicationUserRole applicationUserRole);
        void RemoveUserFromRole(ApplicationUserRole applicationUserRole);
        List<ApplicationUserRole> GetApplicationUserRoles();
        List<ApplicationUserRole> GetApplicationUserRolesByUserId(int userId);
        List<ApplicationUserRole> GetApplicationUserRolesByRoleId(int roleId);
        List<ApplicationUserRole> GetApplicationUserRolesByRoleIdAndUserId(int roleId,int userId);

        #endregion

    }
}
