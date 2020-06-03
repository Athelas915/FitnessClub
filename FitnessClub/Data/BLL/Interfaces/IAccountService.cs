using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<string> GetEmail(string userId);
        Task<byte[]> GetPersonalData(string userId);
        Task<string> GetPhoneNumber(string userId);
        Task<string> GetUsername(string userId);
        Task<bool> IsEmailConfirmed(string userId);
        Task<IdentityResult> DeleteSelfUser(string userId, string inputPassword);
        Task<IdentityResult> SetPhoneNumber(string userId, string newNumber);
        Task<IdentityResult> ChangeEmail(string userId, string newEmail, string code);
    }
}
