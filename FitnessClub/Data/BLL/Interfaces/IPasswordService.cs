using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IPasswordService
    {
        Task<bool?> HasPassword(int userId);
        Task<IdentityResult> AddPassword(int userId, string newPassword);
        Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<IdentityResult> ResetPassword(int userId, string code, string newPassword);
    }
}
