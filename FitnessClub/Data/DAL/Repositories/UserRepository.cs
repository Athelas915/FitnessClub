using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Repositories
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        public UserManager<AspNetUser> UserManager { get; }
        public UserRepository(IUnitOfWork unitOfWork, UserManager<AspNetUser> userManager) : base(unitOfWork)
        {
            UserManager = userManager;
        }
        public async Task<AspNetUser> GetUser(string userId)
        {
            return await UserManager.FindByIdAsync(userId);

        }
        public async Task<AspNetUser> GetUserWithData(string userId)
        {
            var query = UserManager.Users.Where(a => a.Id.ToString() == userId)
                .Include(a => a.Person)
                .ThenInclude(a => a.Address);

            return await query.FirstOrDefaultAsync();
        }
    }
}
