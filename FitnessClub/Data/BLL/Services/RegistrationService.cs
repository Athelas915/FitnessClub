using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository userRepository;
        private readonly SignInManager<AspNetUser> signInManager;
        private readonly ILogger<RegistrationService> logger;
        private readonly int userId;
        public RegistrationService(
            IUserRepository userRepository,
            SignInManager<AspNetUser> signInManager,
            ILogger<RegistrationService> logger,
            UserResolverService userResolver
            )
        {
            this.userRepository = userRepository;
            this.signInManager = signInManager;
            this.logger = logger;
            userId = userResolver.GetUserId();
        }
        public async Task<IdentityResult> CreateUser(string email, Person person, ExternalLoginInfo info, params string[] roles)
        {
            var user = new AspNetUser { UserName = email, Email = email };

            var tr = await userRepository.BeginTransaction();
            var result = await userRepository.UserManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await userRepository.Commit();
                result = await userRepository.UserManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                    await userRepository.Commit();

                    user.Person = person;
                    if (userId == -1)
                    {
                        user.Person.CreatedBy = user.Id;
                        user.Person.Address.CreatedBy = user.Id;
                    }
                    else
                    {
                        user.Person.CreatedBy = userId;
                        user.Person.Address.CreatedBy = userId;
                    }

                    foreach (var r in roles)
                    {
                        await userRepository.UserManager.AddToRoleAsync(user, r);
                    }
                    await signInManager.SignInAsync(user, isPersistent: false);

                }
                await userRepository.Commit();
                await tr.CommitAsync();
            }
            else
            {
                userRepository.Dispose();
            }
            return result;
        }
        public async Task<IdentityResult> CreateUser(string email, string password, Person person, params string[] roles)
        {
            var user = new AspNetUser { UserName = email, Email = email };

            var tr = await userRepository.BeginTransaction();
            var result = await userRepository.UserManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                logger.LogInformation("User created a new account with password.");

                await userRepository.Commit();

                user.Person = person;
                if (userId == -1)
                {
                    user.Person.CreatedBy = user.Id;
                    user.Person.Address.CreatedBy = user.Id;
                }
                else
                {
                    user.Person.CreatedBy = userId;
                    user.Person.Address.CreatedBy = userId;
                }

                foreach (var r in roles)
                {
                    await userRepository.UserManager.AddToRoleAsync(user, r);
                }
                await userRepository.Commit();
                await tr.CommitAsync();
            }
            else
            {
                userRepository.Dispose();
            }
            return result;
        }
        public async Task<IdentityResult> ConfirmEmail(string userId, string code)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await userRepository.UserManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                await userRepository.Commit();
            }
            else
            {
                userRepository.Dispose();
            }
            return result;
        }
        public async Task<bool> ConfirmedAccountRequired(string userId)
        {
            var required = userRepository.UserManager.Options.SignIn.RequireConfirmedAccount;
            if (!required)
            {
                var user = await userRepository.GetUser(userId);
                await signInManager.SignInAsync(user, isPersistent: false);
            }
            return required;
        }
    }
}
