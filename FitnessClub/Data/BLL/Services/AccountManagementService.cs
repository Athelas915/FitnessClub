using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Data.BLL.Services
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IUserRepository userRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly SignInManager<AspNetUser> signInManager;
        public AccountManagementService(
            IUserRepository userRepository, 
            ICustomerRepository customerRepository,
            SignInManager<AspNetUser> signInManager)
        {
            this.userRepository = userRepository;
            this.customerRepository = customerRepository;
            this.signInManager = signInManager;
        }

        public Task<IdentityResult> AddLogin(AspNetUser user, ExternalLoginInfo info)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> AddPassword(AspNetUser user, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ChangeEmail(AspNetUser user, string newEmail, string code)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> ChangePassword(ClaimsPrincipal currentUser, string oldPassword, string newPassword)
        {
            var user = await userRepository.GetUser(currentUser);
            if (user == null)
            {
                
                return null;
            }
            var result = await userRepository.Manager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                await userRepository.Commit();
                await signInManager.RefreshSignInAsync(user);
                Serilog.Log.Information("User with ID '{UserId}' deleted themselves.", await userRepository.Manager.GetUserIdAsync(user));
            }
            return result;
        }

        public Task<IdentityResult> ConfirmEmail(AspNetUser user, string code)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> Create(AspNetUser user, string role)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> DeleteSelfUser(ClaimsPrincipal currentUser, string inputPassword)
        {
            var hasPassword = await HasPassword(currentUser);
            if (hasPassword == null)
            {
                return null;
            }
            var user = await userRepository.Manager.GetUserAsync(currentUser);
            if (hasPassword.Value)
            {
                if (!await userRepository.Manager.CheckPasswordAsync(user, inputPassword))
                {
                    return IdentityResult.Failed(new IdentityError()
                    {
                        Code = "IncorrectPassword",
                        Description = "IncorrectPassword"
                    });
                }
            }
            var result = await userRepository.Manager.DeleteAsync(user);
            var userId = await userRepository.Manager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }
            await userRepository.Commit();
            await signInManager.SignOutAsync();
            Serilog.Log.Information("User with ID '{UserId}' deleted themselves.", userId);

            return result;
        }

        public Task<string> GenerateChangeEmailToken(AspNetUser user, string newEmail)
        {
            throw new NotImplementedException();
        }

        public Task<string> GeneratePasswordResetToken(AspNetUser user)
        {
            throw new NotImplementedException();
        }

        public Task<UserLoginInfo> GetLogins(AspNetUser user)
        {
            throw new NotImplementedException();
        }

        public Task<FileContentResult> GetPersonalData(AspNetUser user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPhoneNumber(AspNetUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool?> HasPassword(ClaimsPrincipal currentUser)
        {
            var user = await userRepository.GetUser(currentUser);
            if (user == null)
            {
                return null;
            }
            return await userRepository.Manager.HasPasswordAsync(user);
        }

        public Task<bool> IsEmailConfirmed(AspNetUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RemoveLogin(AspNetUser user, string loginProvied, string providerKey)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ResetPasswordAsync(AspNetUser user, string code, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> SetPhoneNumber(AspNetUser user, string newNumber)
        {
            throw new NotImplementedException();
        }
    }
}
