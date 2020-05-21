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
        public UserManager<AspNetUser> Manager { get; }
        public UserRepository(IUnitOfWork unitOfWork, UserManager<AspNetUser> userManager) : base(unitOfWork)
        {
            Manager = userManager;
        }

        public async Task<AspNetUser> GetUser(ClaimsPrincipal user)
        {
            return await Manager.GetUserAsync(user);
        }

        public async Task<AspNetUser> GetUser(int userId)
        {
            return await Manager.FindByIdAsync(userId.ToString());
        }

        public async Task<AspNetUser> GetUser(string email)
        {
            return await Manager.FindByEmailAsync(email);
        }
    }
}
