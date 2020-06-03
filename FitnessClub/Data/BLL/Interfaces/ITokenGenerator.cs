using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateEmailConfirmationToken(string userId);
        Task<string> GenerateChangeEmailToken(string userId, string newEmail);
        Task<string> GeneratePasswordResetToken(string userId);
    }
}
