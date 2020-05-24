using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IAccountManagementService
    {
        string GetUserId(ClaimsPrincipal user);
        Task<string> GetUserId(string email);
        Task<IdentityResult> AddLogin(string userId);
        Task<IdentityResult> AddPassword(string userId, string newPassword);
        Task<IdentityResult> ChangeEmail(string userId, string newEmail, string code);
        Task<IdentityResult> ChangePassword(string userId, string oldPassword, string newPassword);
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null);
        Task<bool> ConfirmedAccountRequired(string userId);
        Task<IdentityResult> ConfirmEmail(string userId, string code);
        Task<IdentityResult> CreateUser(string email, Person person, ExternalLoginInfo info, params string[] roles);
        Task<IdentityResult> CreateUser(string email, string password, Person person, params string[] roles);
        Task<IdentityResult> DeleteSelfUser(string userId, string inputPassword);
        Task<SignInResult> ExternalLoginSignIn(ExternalLoginInfo info);
        Task<string> GenerateChangeEmailToken(string userId, string newEmail);
        Task<string> GenerateEmailConfirmationToken(string userId);
        Task<string> GeneratePasswordResetToken(string userId);
        Task<string> GetEmail(string userId);
        Task<ExternalLoginInfo> GetExternalLoginInfo();
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemes();
        Task<(IList<UserLoginInfo>, IList<AuthenticationScheme>)> GetLogins(string userId);
        Task<byte[]> GetPersonalData(string userId);
        Task<string> GetPhoneNumber(string userId);
        Task<string> GetUsername(string userId);
        Task<bool?> HasPassword(string userId);
        Task<bool> IsEmailConfirmed(string userId);
        Task<IdentityResult> RemoveLogin(string userId, string loginProvider, string providerKey);
        Task<IdentityResult> ResetPasswordAsync(string userId, string code, string newPassword);
        Task<IdentityResult> SetPhoneNumber(string userId, string newNumber);
    }
}
