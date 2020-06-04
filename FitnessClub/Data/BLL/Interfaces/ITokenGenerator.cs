using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateEmailConfirmationToken(int userId);
        Task<string> GenerateChangeEmailToken(int userId, string newEmail);
        Task<string> GeneratePasswordResetToken(int userId);
    }
}
