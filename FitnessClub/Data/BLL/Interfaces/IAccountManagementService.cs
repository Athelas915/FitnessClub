using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IAccountManagementService
    {
        Task<IdentityResult> AddLogin(AspNetUser user, ExternalLoginInfo info);
        Task<IdentityResult> AddPassword(AspNetUser user, string newPassword);
        Task<IdentityResult> ChangeEmail(AspNetUser user, string newEmail, string code);
        Task<IdentityResult> ChangePassword(ClaimsPrincipal currentUser, string oldPassword, string newPassword);
        Task<IdentityResult> ConfirmEmail(AspNetUser user, string code);
        Task<IdentityResult> Create(AspNetUser user, string role);
        Task<IdentityResult> DeleteSelfUser(ClaimsPrincipal currentUser, string inputPassword);
        Task<string> GenerateChangeEmailToken(AspNetUser user, string newEmail);
        Task<string> GeneratePasswordResetToken(AspNetUser user);
        Task<UserLoginInfo> GetLogins(AspNetUser user);
        Task<FileContentResult> GetPersonalData(AspNetUser user);
        Task<string> GetPhoneNumber(AspNetUser user);
        Task<bool?> HasPassword(ClaimsPrincipal currentUser);
        Task<bool> IsEmailConfirmed(AspNetUser user);
        Task<IdentityResult> RemoveLogin(AspNetUser user, string loginProvied, string providerKey);
        Task<IdentityResult> ResetPasswordAsync(AspNetUser user, string code, string newPassword);
        Task<IdentityResult> SetPhoneNumber(AspNetUser user, string newNumber);
    }
}
