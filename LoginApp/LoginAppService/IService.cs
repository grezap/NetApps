using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginAppService
{
    public interface IService
    {
        #region Application Users

        List<ApplicationUser> GetUsers();
        ApplicationUser GetUserById(int id);
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
        void InsertRole(ApplicationRole applicationRole);
        void InsertRoles(List<ApplicationRole> applicationRoles);
        void DeleteRole(ApplicationRole applicationRole);
        void DeleteRoles(List<ApplicationRole> applicationRoles);
        void UpdateRole(ApplicationRole applicationRole);
        
        #endregion

    }
}
