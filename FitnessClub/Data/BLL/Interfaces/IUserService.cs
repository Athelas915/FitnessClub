using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IUserService
    {
        //Data Getters
        Task<string> GetEmail(string userId);
        Task<byte[]> GetPersonalData(string userId);
        Task<string> GetPhoneNumber(string userId);
        string GetUserId(ClaimsPrincipal user);
        Task<string> GetUserId(string email);
        Task<string> GetUsername(string userId);
        //Add new data
        Task<IdentityResult> AddPassword(string userId, string newPassword);
        Task<IdentityResult> CreateUser(string email, Person person, ExternalLoginInfo info, params string[] roles);
        Task<IdentityResult> CreateUser(string email, string password, Person person, params string[] roles);
        Task<IdentityResult> SetPhoneNumber(string userId, string newNumber);
        //Edit existing data
        Task<IdentityResult> ChangeEmail(string userId, string newEmail, string code);
        Task<IdentityResult> ChangePassword(string userId, string oldPassword, string newPassword);
        Task<IdentityResult> ConfirmEmail(string userId, string code);
        Task<IdentityResult> ResetPassword(string userId, string code, string newPassword);
        //Remove data
        Task<IdentityResult> DeleteSelfUser(string userId, string inputPassword);
        //State checks
        Task<bool> ConfirmedAccountRequired(string userId);
        Task<bool?> HasPassword(string userId);
        Task<bool> IsEmailConfirmed(string userId);
        //Token generation
        Task<string> GenerateChangeEmailToken(string userId, string newEmail);
        Task<string> GenerateEmailConfirmationToken(string userId);
        Task<string> GeneratePasswordResetToken(string userId);
    }
}
