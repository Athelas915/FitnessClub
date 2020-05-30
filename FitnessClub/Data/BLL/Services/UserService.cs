using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly SignInManager<AspNetUser> signInManager;
        public UserService(
            IUserRepository userRepository,
            SignInManager<AspNetUser> signInManager)
        {
            this.userRepository = userRepository;
            this.signInManager = signInManager;
        }

        //Data getters
        public async Task<string> GetEmail(string userId)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.GetEmailAsync(user);
        }

        public async Task<byte[]> GetPersonalData(string userId)
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

        public async Task<string> GetPhoneNumber(string userId)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }
            return await userRepository.UserManager.GetPhoneNumberAsync(user);
        }

        public async Task<string> GetUsername(string userId)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.GetUserNameAsync(user);
        }
        public string GetUserId(ClaimsPrincipal user)
        {
            var userId = userRepository.UserManager.GetUserId(user);

            return userId;
        }
        public async Task<string> GetUserId(string email)
        {
            var user = await userRepository.UserManager.FindByEmailAsync(email);

            return await userRepository.UserManager.GetUserIdAsync(user);
        }

        //Add new data

        public async Task<IdentityResult> AddPassword(string userId, string newPassword)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            var result = await userRepository.UserManager.AddPasswordAsync(user, newPassword);
            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(user);
                await userRepository.Commit();
            }
            else
            {
                userRepository.Dispose();
            }
            return result;
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
                    Serilog.Log.Information("User created an account using {Name} provider.", info.LoginProvider);
                    await userRepository.Commit();

                    user.Person = person;
                    user.Person.CreatedBy = user.Id;
                    user.Person.Address.CreatedBy = user.Id;

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
                Serilog.Log.Information("User created a new account with password.");

                await userRepository.Commit();

                user.Person = person;
                user.Person.CreatedBy = user.Id;
                user.Person.Address.CreatedBy = user.Id;

                await userRepository.UserManager.AddToRoleAsync(user, "Customer");
                await userRepository.Commit();
                await tr.CommitAsync();
            }
            else
            {
                userRepository.Dispose();
            }
            return result;
        }
        public async Task<IdentityResult> SetPhoneNumber(string userId, string newNumber)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
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

        //Edit existing data

        public async Task<IdentityResult> ChangeEmail(string userId, string newEmail, string code)
        {

            var user = await userRepository.UserManager.FindByIdAsync(userId);
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

        public async Task<IdentityResult> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
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


        public async Task<IdentityResult> ResetPassword(string userId, string code, string newPassword)
        {
            var user = await userRepository.GetUser(userId);
            var result = await userRepository.UserManager.ResetPasswordAsync(user, code, newPassword);
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
        //Remove data

        public async Task<IdentityResult> DeleteSelfUser(string userId, string inputPassword)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
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
                userRepository.Dispose();
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }
            await userRepository.Commit();
            await signInManager.SignOutAsync();
            Serilog.Log.Information("User with ID '{UserId}' deleted themselves.", userId);

            return result;
        }

        //State checks

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


        public async Task<bool?> HasPassword(string userId)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.HasPasswordAsync(user);
        }

        public async Task<bool> IsEmailConfirmed(string userId) => await userRepository.UserManager.IsEmailConfirmedAsync(await userRepository.GetUser(userId));


        //Token generation

        public async Task<string> GenerateChangeEmailToken(string userId, string newEmail)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public async Task<string> GenerateEmailConfirmationToken(string userId)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return await userRepository.UserManager.GenerateEmailConfirmationTokenAsync(user);
        }
        public async Task<string> GeneratePasswordResetToken(string userId)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            return await userRepository.UserManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}
