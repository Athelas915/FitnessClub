using FitnessClub.Data.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<string> GetEmail(int userId);
        Task<byte[]> GetPersonalData(int userId);
        Task<string> GetPhoneNumber(int userId);
        Task<string> GetUsername(int userId);
        Task<bool> IsEmailConfirmed(int userId);
        Task<IdentityResult> DeleteSelfUser(int userId);
        Task<IdentityResult> DeleteSelfUser(int userId, string inputPassword);
        Task<IdentityResult> SetPhoneNumber(int userId, string newNumber);
        Task<IdentityResult> ChangeEmail(int userId, string newEmail, string code);
        Task<PersonViewModel> GetPerson(int userId);
        Task<PersonViewModel> GetWithAddress(int userId);
        Task UpdateAddress(int userId, AddressViewModel inputAddress);
    }
}
