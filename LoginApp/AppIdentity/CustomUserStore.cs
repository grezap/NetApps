﻿using Domain.Entities;
using LoginAppService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppIdentity
{
    public class CustomUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
    {
        private readonly IService _service;

        public CustomUserStore(IService service)
        {
            _service = service;
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            //var role = _service.GetApplicationRoles().Where(r => r.NormalizedName == roleName).FirstOrDefault();
            //var role = _service.GetApplicationRoleByNormalizedName(roleName);
            var role = await _service.GetApplicationRoleByNormalizedNameAsync(roleName);
            var userRole = new ApplicationUserRole { RoleId = role.Id, UserId = user.Id };
            //_service.InsertUserToRole(userRole);
            await _service.InsertUserToRoleAsync(userRole);
            //return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                await _service.InsertUserAsync(user);

                return await Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));
                _service.DeleteUser(user);
                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {

                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            //return Task.FromResult(_service.GetUsers().Result.Where(u=>u.NormalizedEmail == normalizedEmail).FirstOrDefault());
            return Task.FromResult(_service.GetUserByNormalizedEmail(normalizedEmail));
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (!Int32.TryParse(userId, out int id))
                throw new ArgumentOutOfRangeException(nameof(userId), $"{nameof(userId)} is not a valid integer.");

            return Task.FromResult(_service.GetUserById(id));
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            //var users = _service.GetUsers().Result;
            //return Task.FromResult(users.Where(u => u.NormalizedUserName.ToLower() == normalizedUserName.ToLower()).FirstOrDefault());
            return Task.FromResult(_service.GetUserByNormalizedUserName(normalizedUserName));
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName.ToUpper());
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //var userToRoles = _service.GetApplicationUserRoles().Where(ur=>ur.UserId == user.Id);
            //var userToRoles = _service.GetApplicationUserRolesByUserId(user.Id);
            //IList<string> roleNames = _service.GetApplicationRoles().Where(r => userToRoles.Select(ur => ur.RoleId).Contains(r.Id)).Select(r => r.Name).ToList();
            IList<string> roleNames = _service.GetApplicationRolesByUser(user).Select(r => r.Name).ToList();
            return Task.FromResult(roleNames);
        }

        public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.FromResult(user.Id.ToString());
        }

        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.FromResult(user.UserName);
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            //var role = _service.GetApplicationRoles().Where(r => r.Name == roleName).FirstOrDefault();
            var role = _service.GetApplicationRoleByRoleName(roleName);
            //var userToRoles = _service.GetApplicationUserRoles().Where(ur => ur.RoleId == role.Id).ToList();
            var userToRoles = _service.GetApplicationUserRolesByRoleId(role.Id);
            //IList<ApplicationUser> users = _service.GetUsers().Result.Where(u => userToRoles.Select(ur => ur.UserId).Contains(u.Id)).ToList();
            IList<ApplicationUser> users = _service.GetUsersByRole(role);
            return Task.FromResult(users);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            //var role = _service.GetApplicationRoles().Where(r => r.NormalizedName == roleName).FirstOrDefault();
            var role = _service.GetApplicationRoleByNormalizedName(roleName);
            return Task.FromResult(_service.GetApplicationUserRoles().Any(ur => ur.RoleId == role.Id && ur.UserId == user.Id));
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            //var role = _service.GetApplicationRoles().Where(r => r.Name == roleName).FirstOrDefault();
            var role = _service.GetApplicationRoleByRoleName(roleName);
            //ApplicationUserRole urole = _service.GetApplicationUserRoles().Where(ur => ur.RoleId == role.Id && ur.UserId == user.Id).FirstOrDefault();
            ApplicationUserRole urole = _service.GetApplicationUserRolesByRoleIdAndUserId(role.Id, user.Id).FirstOrDefault();
            _service.RemoveUserFromRole(urole);
            return Task.CompletedTask;
        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.UserName = userName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            try
            {
                var u = _service.GetUserById(user.Id);
                u.UserName = user.UserName;
                u.NormalizedUserName = user.NormalizedUserName;
                u.LockoutEnabled = user.LockoutEnabled;
                u.LockoutEnd = user.LockoutEnd;
                u.NormalizedEmail = user.NormalizedEmail;
                u.PasswordHash = user.PasswordHash;
                u.PhoneNumber = user.PhoneNumber;
                u.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                u.SecurityStamp = user.SecurityStamp;
                u.TwoFactorEnabled = user.TwoFactorEnabled;
                _service.UpdateUser(u);
                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {

                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }

        }
    }
}
