using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IRegistrationService
    {
        Task<IdentityResult> CreateUser(string email, Person person, ExternalLoginInfo info, params string[] roles);
        Task<IdentityResult> CreateUser(string email, string password, Person person, params string[] roles);
        Task<IdentityResult> ConfirmEmail(int userId, string code);
        Task<bool> ConfirmedAccountRequired(int userId);
    }
}
