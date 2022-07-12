using System;
using ChealCore.Models;
using Microsoft.AspNetCore.Identity;
using ChealCore.Enums;

namespace ChealCore.Data
{
    public static class DataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedSudoUser(userManager);
        }

        private static void SeedSudoUser(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("cchimzi@appzonegroup.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "cchimzi@appzonegroup.com";
                user.Email = "cchimzi@appzonegroup.com";
                user.FirstName = "Chimzi";
                user.LastName = "Chiorlu";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString()).Wait();
                    userManager.AddToRoleAsync(user, Roles.Admin.ToString()).Wait();
                    userManager.AddToRoleAsync(user, Roles.Basic.ToString()).Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(Roles.SuperAdmin.ToString()).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = Roles.SuperAdmin.ToString();
                role.IsEnabled = true;
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(Roles.Admin.ToString()).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = Roles.Admin.ToString();
                role.IsEnabled = true;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(Roles.Basic.ToString()).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = Roles.Basic.ToString();
                role.IsEnabled = true;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

        }


    }
}

