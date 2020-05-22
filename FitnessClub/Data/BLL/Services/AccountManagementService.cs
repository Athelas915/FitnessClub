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
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace FitnessClub.Data.BLL.Services
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IUserRepository userRepository;
        private readonly SignInManager<AspNetUser> signInManager;
        public AccountManagementService(
            IUserRepository userRepository,
            SignInManager<AspNetUser> signInManager)
        {
            this.userRepository = userRepository;
            this.signInManager = signInManager;
        }
        public int GetUserId(ClaimsPrincipal user)
        {
            var userId = userRepository.UserManager.GetUserId(user);
            if (userId == null)
            {
                return -1;
            }
            return int.Parse(userId);
        }
        public async Task<int> GetUserId(string email)
        {
            var user = await userRepository.UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                return -1;
            }
            return int.Parse(await userRepository.UserManager.GetUserIdAsync(user));
        }
        public async Task<IdentityResult> AddLogin(int userId)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            var info = await signInManager.GetExternalLoginInfoAsync(userId.ToString());
            if (info == null)
            {
                throw new InvalidOperationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }

            return await userRepository.UserManager.AddLoginAsync(user, info);
        }

        public async Task<IdentityResult> AddPassword(int userId, string newPassword)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            var result = await userRepository.UserManager.AddPasswordAsync(user, newPassword);
            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(user);
            }
            return result;
        }

        public Task<IdentityResult> ChangeEmail(int userId, string newEmail, string code)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            var result = await userRepository.UserManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                await userRepository.Commit();
                await signInManager.RefreshSignInAsync(user);
                Serilog.Log.Information("User with ID '{UserId}' deleted themselves.", await userRepository.UserManager.GetUserIdAsync(user));
            }
            return result;
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, int userId)
        {
            return signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId.ToString());
        }

        public Task<IdentityResult> ConfirmEmail(int userId, string code)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> Create(int userId, string role)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> DeleteSelfUser(int userId, string inputPassword)
        {
            var user = await userRepository.GetUser(userId);
            var hasPassword = await HasPassword(userId);
            if (hasPassword == null)
            {
                return null;
            }
            if (hasPassword.Value)
            {
                if (!await userRepository.UserManager.CheckPasswordAsync(user, inputPassword))
                {
                    return IdentityResult.Failed(new IdentityError()
                    {
                        Code = "IncorrectPassword",
                        Description = "IncorrectPassword"
                    });
                }
            }
            var result = await userRepository.UserManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }
            await userRepository.Commit();
            await signInManager.SignOutAsync();
            Serilog.Log.Information("User with ID '{UserId}' deleted themselves.", userId);

            return result;
        }

        public async Task<string> GenerateChangeEmailToken(int userId, string newEmail)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public async Task<string> GenerateEmailConfirmationToken(int userId)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.GenerateEmailConfirmationTokenAsync(user);
        }
        public Task<string> GeneratePasswordResetToken(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetEmail(int userId)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.GetEmailAsync(user);
        }

        public async Task<(IList<UserLoginInfo>, IList<AuthenticationScheme>)> GetLogins(int userId)
        {
            var user = await userRepository.GetUserWithData(userId);
            if (user == null)
            {
                return (null, null);
            }
            var currentLogins = await userRepository.UserManager.GetLoginsAsync(user);
            var otherLogins = (await signInManager.GetExternalAuthenticationSchemesAsync())
                .Where(auth => currentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            return (currentLogins, otherLogins);
        }

        public async Task<byte[]> GetPersonalData(int userId)
        {
            var user = await userRepository.GetUserWithData(userId);
            if (user == null)
            {
                return null;
            }
            Serilog.Log.Information("User with ID '{UserId}' asked for their personal data.", userId);
            //only include personal data for download

            var allData = new Dictionary<string, Dictionary<string, string>>();

            var accountData = new Dictionary<string, string>();
            var accountDataProps = typeof(AspNetUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in accountDataProps)
            {
                accountData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }
            allData.Add("Account Information", accountData);

            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(Person).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                if (p.Name == "Birthdate")
                {
                    var a = ((DateTime)p.GetValue(user.Person)).ToShortDateString();
                    personalData.Add(p.Name, a);
                }
                else
                {
                    personalData.Add(p.Name, p.GetValue(user.Person)?.ToString() ?? "null");
                }
            }
            allData.Add("Personal information", personalData);

            var addressData = new Dictionary<string, string>();
            var addressDataProps = typeof(Address).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in addressDataProps)
            {
                addressData.Add(p.Name, p.GetValue(user.Person.Address)?.ToString() ?? "null");
            }
            allData.Add("Address information", addressData);

            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(allData));
        }
        
        public async Task<string> GetPhoneNumber(int userId)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.GetPhoneNumberAsync(user);
        }

        public async Task<string> GetUsername(int userId)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.GetUserNameAsync(user);
        }
        public async Task<bool?> HasPassword(int userId)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.HasPasswordAsync(user);
        }

        public async Task<bool> IsEmailConfirmed(int userId) => await userRepository.UserManager.IsEmailConfirmedAsync(await userRepository.GetUser(userId));

        public async Task<IdentityResult> RemoveLogin(int userId, string loginProvider, string providerKey)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            var result = await userRepository.UserManager.RemoveLoginAsync(user, loginProvider, providerKey);

            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(user);
            }
            return result;
        }

        public Task<IdentityResult> ResetPasswordAsync(int userId, string code, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> SetPhoneNumber(int userId, string newNumber)
        {
            var user = await userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }
            var result = await userRepository.UserManager.SetPhoneNumberAsync(user, newNumber);
            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(user);
            }
            return result;
        }
    }
}
