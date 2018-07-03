using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWebApp.Helper
{
    public static class DbInitializer
    {

        public static async Task InitializeDbAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            string[] roleNames = { "Admin", "Member" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new ApplicationRole {Name = roleName, NormalizedName = roleName.ToUpper() });
                }
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("admin");
            if (user == null)
            {
                string username = "admin";
                string password = "123456";
                var success = await userManager.CreateAsync(new ApplicationUser { UserName = username }, password);
                if (success.Succeeded)
                {
                    await userManager.AddToRoleAsync(await userManager.FindByNameAsync(username), "admin");
                }
            }



        }
    }
}
