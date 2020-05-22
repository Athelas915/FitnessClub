using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
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
        public async Task<AspNetUser> GetUser(int userId)
        {
            return await UserManager.FindByIdAsync(userId.ToString());

        }
        public async Task<AspNetUser> GetUserWithData(int userId)
        {
            var query = UserManager.Users.Where(a => a.Id == userId)
                .Include(a => a.Person)
                .ThenInclude(a => a.Address);

            return await query.FirstOrDefaultAsync();
        }
    }
}
