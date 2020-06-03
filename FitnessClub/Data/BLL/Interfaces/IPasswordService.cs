using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IPasswordService
    {
        Task<bool?> HasPassword(string userId);
        Task<IdentityResult> AddPassword(string userId, string newPassword);
        Task<IdentityResult> ChangePassword(string userId, string oldPassword, string newPassword);
        Task<IdentityResult> ResetPassword(string userId, string code, string newPassword);
    }
}
