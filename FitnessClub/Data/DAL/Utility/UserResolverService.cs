using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FitnessClub.Data.DAL.Utility
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserResolverService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
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
                userId = "-1";
            }
            return int.Parse(userId);
            
        }
    }
}
