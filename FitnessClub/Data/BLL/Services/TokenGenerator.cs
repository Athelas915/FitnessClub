using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<AspNetUser> userManager;
        public TokenGenerator(IUserRepository userRepository)
        {
            userManager = userRepository.UserManager;
        }
        public async Task<string> GenerateChangeEmailToken(string userId, string newEmail)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return await userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public async Task<string> GenerateEmailConfirmationToken(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        public async Task<string> GeneratePasswordResetToken(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}
