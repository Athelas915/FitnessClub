using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

//this class generates users in the database. Useful for development.

namespace FitnessClub.Data.DAL.Utility
{
    public static class IdentityDataInitializer
    {
        public static async Task Save(IUnitOfWork unitOfWork)
        {
            await unitOfWork.Save();
            return;
        }
        public static void SeedData(FCContext context, UserManager<AspNetUser> userManager, RoleManager<AspNetRole> roleManager)
        {
            SeedRoles(context, roleManager);
            SeedUsers(context, userManager);
        }
        public static void SeedUsers(FCContext context, UserManager<AspNetUser> userManager)
        {
            var tr = context.Database.BeginTransaction();
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
                    context.SaveChanges();
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                    context.SaveChanges();
                }

            }
            /*
            if (userManager.FindByNameAsync
         ("Coach1").Result == null)
            {
                AspNetUser user = new AspNetUser();
                user.UserName = "coach1@fitness.club";
                user.Email = "coach1@fitness.club";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, "Coach12#$").Result;

                if (result.Succeeded)
                {
                    context.SaveChanges();
                    userManager.AddToRoleAsync(user, "Coach").Wait();
                    context.SaveChanges();
                }
            }
            if (userManager.FindByNameAsync
         ("Coach3").Result == null)
            {
                AspNetUser user = new AspNetUser();
                user.UserName = "coach3@fitness.club";
                user.Email = "coach3@fitness.club";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, "Coach12#$").Result;

                if (result.Succeeded)
                {
                    context.SaveChanges();
                    userManager.AddToRoleAsync(user, "Coach").Wait();
                    context.SaveChanges();
                }
            }
            if (userManager.FindByNameAsync
         ("Coach2").Result == null)
            {
                AspNetUser user = new AspNetUser();
                user.UserName = "coach2@fitness.club";
                user.Email = "coach2@fitness.club";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, "Coach12#$").Result;

                if (result.Succeeded)
                {
                    context.SaveChanges();
                    userManager.AddToRoleAsync(user, "Coach").Wait();
                    context.SaveChanges();
                }
            }*/
            tr.Commit();
        }
        public static void SeedRoles(FCContext context, RoleManager<AspNetRole> roleManager)
        {
            var tr = context.Database.BeginTransaction();
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
                context.SaveChanges();
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
                context.SaveChanges();
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
                context.SaveChanges();
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
                context.SaveChanges();
            }
            tr.Commit();
        }
    }
}
