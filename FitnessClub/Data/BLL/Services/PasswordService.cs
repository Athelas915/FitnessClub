using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IUserRepository userRepository;
        private readonly SignInManager<AspNetUser> signInManager;
        private readonly ILogger<PasswordService> logger;
        public PasswordService(IUserRepository userRepository, SignInManager<AspNetUser> signInManager, ILogger<PasswordService> logger)
        {
            this.userRepository = userRepository;
            this.signInManager = signInManager;
            this.logger = logger;
        }
        public async Task<IdentityResult> AddPassword(int userId, string newPassword)
        {
            var user = await userRepository.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return null;
            }
            var result = await userRepository.AddPasswordAsync(user, newPassword);
            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(user);
                await userRepository.Commit();
            }
            else
            {
                userRepository.Dispose();
            }
            return result;
        }
        public async Task<bool?> HasPassword(int userId)
        {
            var user = await userRepository.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return null;
            }
            return await userRepository.HasPasswordAsync(user);
        }
        public async Task<IdentityResult> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await userRepository.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return null;
            }
            var result = await userRepository.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                await userRepository.Commit();
                await signInManager.RefreshSignInAsync(user);
                logger.LogInformation("User with ID '{UserId}' deleted themselves.", await userRepository.GetUserIdAsync(user));
            }
            else
            {
                userRepository.Dispose();
            }
            return result;
        }
        public async Task<IdentityResult> ResetPassword(int userId, string code, string newPassword)
        {
            var user = await userRepository.GetUser(userId.ToString());
            var result = await userRepository.ResetPasswordAsync(user, code, newPassword);
            if (result.Succeeded)
            {
                await userRepository.Commit();
            }
            else
            {
                userRepository.Dispose();
            }
            return result;
        }
    }
}
