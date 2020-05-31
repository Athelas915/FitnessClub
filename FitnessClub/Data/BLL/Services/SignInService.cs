using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class SignInService : ISignInService
    {
        private readonly IUserRepository userRepository;
        private readonly SignInManager<AspNetUser> signInManager;
        public SignInService(
            IUserRepository userRepository,
            SignInManager<AspNetUser> signInManager)
        {
            this.userRepository = userRepository;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> AddLogin(string userId)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            var info = await signInManager.GetExternalLoginInfoAsync(userId);
            if (info == null)
            {
                throw new InvalidOperationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }
            var result = await userRepository.UserManager.AddLoginAsync(user, info);
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
        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null) =>
            signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);


        public async Task<SignInResult> ExternalLoginSignIn(ExternalLoginInfo info) =>
            await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemes() => await signInManager.GetExternalAuthenticationSchemesAsync();

        public async Task<ExternalLoginInfo> GetExternalLoginInfo() => await signInManager.GetExternalLoginInfoAsync();

        public async Task<(IList<UserLoginInfo>, IList<AuthenticationScheme>)> GetLogins(string userId)
        {
            var user = await userRepository.GetUserWithData(userId);
            if (user == null)
            {
                return (null, null);
            }
            var currentLogins = await userRepository.UserManager.GetLoginsAsync(user);
            var otherLogins = (await GetExternalAuthenticationSchemes())
                .Where(auth => currentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            return (currentLogins, otherLogins);
        }

        public async Task<IdentityResult> RemoveLogin(string userId, string loginProvider, string providerKey)
        {
            var user = await userRepository.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            var result = await userRepository.UserManager.RemoveLoginAsync(user, loginProvider, providerKey);

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

        public async Task SignOut() => await signInManager.SignOutAsync();
        public async Task<SignInResult> SignIn(string email, string password, bool rememberMe, bool lockoutOnFailure = false) => await signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure);
    }
}
