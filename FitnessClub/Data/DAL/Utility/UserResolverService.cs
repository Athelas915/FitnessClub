using FitnessClub.Data.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Utility
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserResolverService> logger;

        public UserResolverService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, ILogger<UserResolverService> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
            this.logger = logger;
        }
        public string GetUser()
        {
            return httpContextAccessor.HttpContext.User?.Identity?.Name;
        }
        public int GetUserId()
        {
            String userId;
            try
            {
                userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (NullReferenceException)
            {
                logger.LogInformation($"Couldn't find id of the logged in customer.");
                userId = "-1";
            }
            return int.Parse(userId);

        }
        public string GetUserId(ClaimsPrincipal user)
        {
            var userId = userRepository.UserManager.GetUserId(user);

            return userId;
        }
        public async Task<string> GetUserId(string email)
        {
            var user = await userRepository.UserManager.FindByEmailAsync(email);

            return await userRepository.UserManager.GetUserIdAsync(user);
        }
    }
}