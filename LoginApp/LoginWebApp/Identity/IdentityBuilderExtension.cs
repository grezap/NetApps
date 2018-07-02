using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWebApp.Identity
{
    public static class IdentityBuilderExtension
    {
        public static IdentityBuilder AddCustomStores(this IdentityBuilder builder)
        {
            builder.Services.AddTransient<IUserStore<ApplicationUser>, CustomUserStore>();
            builder.Services.AddTransient<IRoleStore<IdentityRole>, CustomRoleStore>();
            return builder;
        }
    }
}
