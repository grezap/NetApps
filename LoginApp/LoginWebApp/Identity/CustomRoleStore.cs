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
    public class CustomRoleStore : IRoleStore<IdentityRole>
    {

        private readonly IService _service;

        public CustomRoleStore(IService service)
        {
            _service = service;
        }

        public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            try
            {
                if (role == null)
                    throw new ArgumentNullException(nameof(role));

                var rol = getRoleEntity(role);

                _service.InsertRole(rol);

                return Task.FromResult(IdentityResult.Success);

            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));

            }
        }

        public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            try
            {
                var rol = getRoleEntity(role);
                _service.DeleteRole(rol);
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

        public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(roleId))
                throw new ArgumentNullException(nameof(roleId));

            if (!int.TryParse(roleId, out int id))
                throw new ArgumentOutOfRangeException(nameof(roleId), $"{nameof(roleId)} is not a valid int");

            var rol = _service.GetApplicationRoles().Where(r => r.AppRlId == id).FirstOrDefault();

            return Task.FromResult(getIdentityRole(rol));

        }

        public Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.Name = roleName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            try
            {
                var rol = getRoleEntity(role);

                _service.UpdateRole(rol);

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {

                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }
        }

        #region Private Methods
        private ApplicationRole getRoleEntity(IdentityRole value)
        {
            return value == null
                ? default(ApplicationRole)
                : new ApplicationRole
                {
                    AppRlId = Convert.ToInt64(value.Id),
                    AppRlName = value.Name
                };
        }

        private IdentityRole getIdentityRole(ApplicationRole value)
        {
            return value == null
                ? default(IdentityRole)
                : new IdentityRole
                {
                    Id = Convert.ToString(value.AppRlId),
                    Name = value.AppRlName
                };
        }
        #endregion

    }
}
