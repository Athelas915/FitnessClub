using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Repositories
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        private readonly UserManager<AspNetUser> userManager;
        public UserRepository(IUnitOfWork unitOfWork, UserManager<AspNetUser> userManager) : base(unitOfWork)
        {
            this.userManager = userManager;
        }
        public async Task<AspNetUser> GetUser(string userId)
        {
            return await userManager.FindByIdAsync(userId);

        }
        public async Task<AspNetUser> GetUserWithData(string userId)
        {
            var query = userManager.Users.Where(a => a.Id.ToString() == userId)
                .Include(a => a.Person)
                .ThenInclude(a => a.Address);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> IsEmailConfirmedAsync(AspNetUser user) => await userManager.IsEmailConfirmedAsync(user);
        public async Task<AspNetUser> FindByIdAsync(string userId) => await userManager.FindByIdAsync(userId);
        public async Task<IdentityResult> SetUserNameAsync(AspNetUser user, string newEmail) => await userManager.SetUserNameAsync(user, newEmail);
        public async Task<IdentityResult> SetPhoneNumberAsync(AspNetUser user, string newNumber) => await userManager.SetPhoneNumberAsync(user, newNumber);
        public async Task<IdentityResult> DeleteAsync(AspNetUser user) => await userManager.DeleteAsync(user);
        public async Task<IdentityResult> ChangeEmailAsync(AspNetUser user, string newEmail, string code) => await userManager.ChangeEmailAsync(user, newEmail, code);
        public async Task<AspNetUser> FindByEmailAsync(string email) => await userManager.FindByEmailAsync(email);
        public async Task<string> GetUserNameAsync(AspNetUser user) => await userManager.GetUserNameAsync(user);
        public async Task<string> GetPhoneNumberAsync(AspNetUser user) => await userManager.GetPhoneNumberAsync(user);
        public async Task<string> GetEmailAsync(AspNetUser user) => await userManager.GetEmailAsync(user);
        public async Task<bool> CheckPasswordAsync(AspNetUser user, string inputPassword) => await userManager.CheckPasswordAsync(user, inputPassword);
        public async Task<IdentityResult> ResetPasswordAsync(AspNetUser user, string code, string newPassword) => await userManager.ResetPasswordAsync(user, code, newPassword);
        public async Task<string> GetUserIdAsync(AspNetUser user) => await userManager.GetUserIdAsync(user);
        public async Task<bool> HasPasswordAsync(AspNetUser user) => await userManager.HasPasswordAsync(user);
        public async Task<IdentityResult> AddPasswordAsync(AspNetUser user, string newPassword) => await userManager.AddPasswordAsync(user, newPassword);
        public async Task<IdentityResult> ChangePasswordAsync(AspNetUser user, string oldPassword, string newPassword) => await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        public async Task<IdentityResult> CreateAsync(AspNetUser user) => await userManager.CreateAsync(user);
        public async Task<IdentityResult> CreateAsync(AspNetUser user, string password) => await userManager.CreateAsync(user, password);
        public async Task<IdentityResult> ConfirmEmailAsync(AspNetUser user, string code) => await userManager.ConfirmEmailAsync(user, code);
        public bool RequireConfirmedAccount() => userManager.Options.SignIn.RequireConfirmedAccount;
        public async Task<IdentityResult> AddLoginAsync(AspNetUser user, ExternalLoginInfo info) => await userManager.AddLoginAsync(user, info);
        public async Task<IdentityResult> AddToRoleAsync(AspNetUser user, string role) => await userManager.AddToRoleAsync(user, role);
        public async Task<IdentityResult> RemoveLoginAsync(AspNetUser user, string loginProvider, string providerKey) => await userManager.RemoveLoginAsync(user, loginProvider, providerKey);
        public async Task<IList<UserLoginInfo>> GetLoginsAsync(AspNetUser user) => await userManager.GetLoginsAsync(user);
        public async Task<string> GenerateChangeEmailTokenAsync(AspNetUser user, string newEmail) => await userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        public async Task<string> GenerateEmailConfirmationTokenAsync(AspNetUser user) => await userManager.GenerateEmailConfirmationTokenAsync(user);
        public async Task<string> GeneratePasswordResetTokenAsync(AspNetUser user) => await userManager.GeneratePasswordResetTokenAsync(user);
    }
}
