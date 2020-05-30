using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IUserRepository : IRepository
    {
        UserManager<AspNetUser> UserManager { get; }
        Task<AspNetUser> GetUser(string userId);
        Task<AspNetUser> GetUserWithData(string userId);
    }
}
