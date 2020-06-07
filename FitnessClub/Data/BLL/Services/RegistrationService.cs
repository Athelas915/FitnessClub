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
            var result = await userRepository.CreateAsync(user);

            if (result.Succeeded)
            {
                await userRepository.Commit();
                result = await userRepository.AddLoginAsync(user, info);
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
                        await userRepository.AddToRoleAsync(user, r);
                    }

                }
                await userRepository.Commit();
                await tr.CommitAsync();
                await signInManager.SignInAsync(user, isPersistent: false);
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
            var result = await userRepository.CreateAsync(user, password);

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
                    await userRepository.AddToRoleAsync(user, r);
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
        public async Task<IdentityResult> ConfirmEmail(int userId, string code)
        {
            var user = await userRepository.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return null;
            }
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await userRepository.ConfirmEmailAsync(user, code);
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
        public async Task<bool> ConfirmedAccountRequired(int userId)
        {
            var required = userRepository.RequireConfirmedAccount();
            if (!required)
            {
                var user = await userRepository.GetUser(userId.ToString());
                await signInManager.SignInAsync(user, isPersistent: false);
            }
            return required;
        }
    }
}
