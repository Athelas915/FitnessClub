using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FitnessClub.Data.Models.Identity;

namespace FitnessClub.Data.DAL
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<AspNetUser> userManager, RoleManager<AspNetRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        public static void SeedUsers(UserManager<AspNetUser> userManager)
        {
            if (userManager.FindByNameAsync
         ("Admin").Result == null)
            {
                AspNetUser user = new AspNetUser();
                user.UserName = "admin@fitness.club";
                user.Email = "admin@fitness.club";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, Startup.Configuration["DefaultAdminPassword"]).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
        public static void SeedRoles(RoleManager<AspNetRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
        ("Administrator").Result)
            {
                AspNetRole role = new AspNetRole();
                role.Id = 1;
                role.Name = "Administrator";
                role.NormalizedName = "ADMINISTRATOR";
                role.ConcurrencyStamp = "0";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
        ("Customer").Result)
            {
                AspNetRole role = new AspNetRole();
                role.Id = 2;
                role.Name = "Customer";
                role.NormalizedName = "CUSTOMER";
                role.ConcurrencyStamp = "0";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
        ("Employee").Result)
            {
                AspNetRole role = new AspNetRole();
                role.Id = 3;
                role.Name = "Employee";
                role.NormalizedName = "EMPLOYEE";
                role.ConcurrencyStamp = "0";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
        ("Coach").Result)
            {
                AspNetRole role = new AspNetRole();
                role.Id = 4;
                role.Name = "Coach";
                role.NormalizedName = "COACH";
                role.ConcurrencyStamp = "0";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
