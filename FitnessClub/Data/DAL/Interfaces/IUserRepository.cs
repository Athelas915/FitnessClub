using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IUserRepository : IRepository
    {
        //this is a decorator for UserManager<T> class. The decorator makes sure this registers in unit of work like other repositories.
        Task<AspNetUser> GetUser(string userId);
        Task<AspNetUser> GetUserWithData(string userId);
        Task<bool> IsEmailConfirmedAsync(AspNetUser user);
        Task<AspNetUser> FindByIdAsync(string userId);
        Task<IdentityResult> SetUserNameAsync(AspNetUser user, string newEmail);
        Task<IdentityResult> SetPhoneNumberAsync(AspNetUser user, string newNumber);
        Task<IdentityResult> DeleteAsync(AspNetUser user);
        Task<IdentityResult> ChangeEmailAsync(AspNetUser user, string newEmail, string code);
        Task<AspNetUser> FindByEmailAsync(string email);
        Task<string> GetUserNameAsync(AspNetUser user);
        Task<string> GetPhoneNumberAsync(AspNetUser user);
        Task<string> GetEmailAsync(AspNetUser user);
        Task<bool> CheckPasswordAsync(AspNetUser user, string inputPassword);
        Task<IdentityResult> ResetPasswordAsync(AspNetUser user, string code, string newPassword);
        Task<string> GetUserIdAsync(AspNetUser user);
        Task<bool> HasPasswordAsync(AspNetUser user);
        Task<IdentityResult> AddPasswordAsync(AspNetUser user, string newPassword);
        Task<IdentityResult> ChangePasswordAsync(AspNetUser user, string oldPassword, string newPassword);
        Task<IdentityResult> CreateAsync(AspNetUser user);
        Task<IdentityResult> CreateAsync(AspNetUser user, string password);
        Task<IdentityResult> ConfirmEmailAsync(AspNetUser user, string code);
        bool RequireConfirmedAccount();
        Task<IdentityResult> AddLoginAsync(AspNetUser user, ExternalLoginInfo info);
        Task<IdentityResult> AddToRoleAsync(AspNetUser user, string role);
        Task<IdentityResult> RemoveLoginAsync(AspNetUser user, string loginProvider, string providerKey);
        Task<IList<UserLoginInfo>> GetLoginsAsync(AspNetUser user);
        Task<string> GenerateChangeEmailTokenAsync(AspNetUser user, string newEmail);
        Task<string> GenerateEmailConfirmationTokenAsync(AspNetUser user);
        Task<string> GeneratePasswordResetTokenAsync(AspNetUser user);
    }
}
