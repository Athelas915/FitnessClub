using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        private readonly SignInManager<AspNetUser> signInManager;
        private readonly ILogger<AccountService> logger;
        public AccountService(
            IUserRepository userRepository,
            SignInManager<AspNetUser> signInManager,
            ILogger<AccountService> logger)
        {
            this.userRepository = userRepository;
            this.signInManager = signInManager;
            this.logger = logger;
        }
        public async Task<string> GetEmail(int userId)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {   
                return null;
            }
            return await userRepository.UserManager.GetEmailAsync(user);
        }
        public async Task<byte[]> GetPersonalData(int userId)
        {
            var user = await userRepository.GetUserWithData(userId.ToString());
            if (user == null)
            {
                return null;
            }
            logger.LogInformation("User with ID '{UserId}' asked for their personal data.", userId);
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
            var user = await userRepository.UserManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new InvalidOperationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }
            return await userRepository.UserManager.GetPhoneNumberAsync(user);
        }

        public async Task<string> GetUsername(int userId)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.GetUserNameAsync(user);
        }
        public async Task<IdentityResult> SetPhoneNumber(int userId, string newNumber)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return null;
            }
            var result = await userRepository.UserManager.SetPhoneNumberAsync(user, newNumber);
            if (result.Succeeded)
            {
                await userRepository.Commit();
                await signInManager.RefreshSignInAsync(user);
            }
            else
            {
                userRepository.Dispose();
            }
            return result;
        }
        public async Task<IdentityResult> ChangeEmail(int userId, string newEmail, string code)
        {

            var user = await userRepository.UserManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return null;
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await userRepository.UserManager.ChangeEmailAsync(user, newEmail, code);
            if (!result.Succeeded)
            {
                userRepository.Dispose();
                return result;
            }

            // In our UI email and user name are one and the same, so when we update the email
            // we need to update the user name.
            var setUserNameResult = await userRepository.UserManager.SetUserNameAsync(user, newEmail);
            if (!setUserNameResult.Succeeded)
            {
                userRepository.Dispose();
                return result;
            }

            await signInManager.RefreshSignInAsync(user);

            await userRepository.Commit();

            return result;
        }
        public async Task<IdentityResult> DeleteSelfUser(int userId)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId.ToString());
            var result = await userRepository.UserManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                userRepository.Dispose();
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }
            await userRepository.Commit();
            await signInManager.SignOutAsync();
            logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return result;
        }
        public async Task<IdentityResult> DeleteSelfUser(int userId, string inputPassword)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId.ToString());
            if (!await userRepository.UserManager.CheckPasswordAsync(user, inputPassword))
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "IncorrectPassword",
                    Description = "IncorrectPassword"
                });
            }
            var result = await userRepository.UserManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                userRepository.Dispose();
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }
            await userRepository.Commit();
            await signInManager.SignOutAsync();
            logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return result;
        }
        public async Task<bool> IsEmailConfirmed(int userId) => await userRepository.UserManager.IsEmailConfirmedAsync(await userRepository.GetUser(userId.ToString()));
    }
}
