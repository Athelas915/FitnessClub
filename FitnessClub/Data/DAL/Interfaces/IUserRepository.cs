using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IUserRepository : IRepository
    {
        UserManager<AspNetUser> UserManager { get; }
        Task<AspNetUser> GetUser(int userId);
        Task<AspNetUser> GetUserWithData(int userId);
    }
}
