using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IAccountManagementService
    {
        int GetUserId(ClaimsPrincipal user);
        Task<int> GetUserId(string email);
        Task<IdentityResult> AddLogin(int userId);
        Task<IdentityResult> AddPassword(int userId, string newPassword);
        Task<IdentityResult> ChangeEmail(int userId, string newEmail, string code);
        Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword);
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, int userId);
        Task<IdentityResult> ConfirmEmail(int userId, string code);
        Task<IdentityResult> Create(int userId, string role);
        Task<IdentityResult> DeleteSelfUser(int userId, string inputPassword);
        Task<string> GenerateChangeEmailToken(int userId, string newEmail);
        Task<string> GenerateEmailConfirmationToken(int userId);
        Task<string> GeneratePasswordResetToken(int userId);
        Task<string> GetEmail(int userId);
        Task<(IList<UserLoginInfo>, IList<AuthenticationScheme>)> GetLogins(int userId);
        Task<byte[]> GetPersonalData(int userId);
        Task<string> GetPhoneNumber(int userId);
        Task<string> GetUsername(int userId);
        Task<bool?> HasPassword(int userId);
        Task<bool> IsEmailConfirmed(int userId);
        Task<IdentityResult> RemoveLogin(int userId, string loginProvider, string providerKey);
        Task<IdentityResult> ResetPasswordAsync(int userId, string code, string newPassword);
        Task<IdentityResult> SetPhoneNumber(int userId, string newNumber);
    }
}
