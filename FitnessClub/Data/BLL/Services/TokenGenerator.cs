using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IUserRepository userRepository;
        public TokenGenerator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<string> GenerateChangeEmailToken(int userId, string newEmail)
        {
            var user = await userRepository.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return null;
            }
            return await userRepository.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public async Task<string> GenerateEmailConfirmationToken(int userId)
        {
            var user = await userRepository.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return null;
            }
            return await userRepository.GenerateEmailConfirmationTokenAsync(user);
        }
        public async Task<string> GeneratePasswordResetToken(int userId)
        {
            var user = await userRepository.FindByIdAsync(userId.ToString());
            return await userRepository.GeneratePasswordResetTokenAsync(user);
        }
    }
}
