using System;
using ChealCore.Models;
using Microsoft.AspNetCore.Identity;
using ChealCore.Enums;
using System.Net;

namespace ChealCore.Data
{
    public static class DataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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
                user.ProfilePicture = GenerateByteArray();

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString()).Wait();
                    userManager.AddToRoleAsync(user, Roles.Admin.ToString()).Wait();
                    userManager.AddToRoleAsync(user, Roles.Basic.ToString()).Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(Roles.SuperAdmin.ToString()).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = Roles.SuperAdmin.ToString();
                //role.IsEnabled = true;
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(Roles.Admin.ToString()).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = Roles.Admin.ToString();
                //role.IsEnabled = true;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(Roles.Basic.ToString()).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = Roles.Basic.ToString();
                //role.IsEnabled = true;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

        }



        public static byte[] GenerateByteArray()
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] profilePicture = webClient.DownloadData("https://fakeimg.pl/250x250/?retina=1&text=CC&font=noto");
                    return profilePicture;
                }
                catch
                {
                    throw new InvalidOperationException($"Can't create placeholder image for new user '{nameof(ApplicationUser)}'");
                }
            }

        }

    }
}

