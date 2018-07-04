using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppIdentity
{
    public static class IdentityBuilderExtension
    {
        public static IdentityBuilder AddCustomIdentityStores(this IdentityBuilder builder)
        {
            builder.Services.AddTransient<IUserStore<ApplicationUser>, CustomUserStore>();
            builder.Services.AddTransient<IRoleStore<ApplicationRole>, CustomRoleStore>();
            return builder;
        }
    }
}
