using DataAccess.UOW;
using Domain.Entities;
using LoginAppService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoginWebApp.Identity
{
    public class CustomRoleStore : IRoleStore<ApplicationRole>
    {

        private readonly IService _service;

        public CustomRoleStore(IService service)
        {
            _service = service;
        }

        public Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            try
            {
                if (role == null)
                    throw new ArgumentNullException(nameof(role));

                //var rol = getRoleEntity(role);

                _service.InsertRole(role);

                return Task.FromResult(IdentityResult.Success);

            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));

            }
        }

        public Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            try
            {
                //var rol = getRoleEntity(role);
                _service.DeleteRole(role);
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

        public Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(roleId))
                throw new ArgumentNullException(nameof(roleId));

            if (!int.TryParse(roleId, out int id))
                throw new ArgumentOutOfRangeException(nameof(roleId), $"{nameof(roleId)} is not a valid int");

            //var rol = _service.GetApplicationRoles().Where(r => r.Id == id).FirstOrDefault();
            var rol = _service.GetApplicationRole(id);
            return Task.FromResult(rol);

        }

        public Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            //var roles = _service.GetApplicationRoles();
            //return Task.FromResult(roles.Where(r=> r.NormalizedName == normalizedRoleName).FirstOrDefault());
            return Task.FromResult(_service.GetApplicationRoleByNormalizedName(normalizedRoleName));
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.Name = roleName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            try
            {
                //var rol = getRoleEntity(role);

                _service.UpdateRole(role);

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {

                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }
        }

        #region Private Methods
        private ApplicationRole getRoleEntity(ApplicationRole value)
        {
            return value == null
                ? default(ApplicationRole)
                : new ApplicationRole
                {
                    Id = value.Id,
                    Name = value.Name
                };
        }

        private IdentityRole getIdentityRole(ApplicationRole value)
        {
            return value == null
                ? default(IdentityRole)
                : new IdentityRole
                {
                    Id = Convert.ToString(value.Id),
                    Name = value.Name
                };
        }
        #endregion

    }
}
