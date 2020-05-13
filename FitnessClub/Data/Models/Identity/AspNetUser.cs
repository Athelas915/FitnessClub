using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FitnessClub.Data.Models.Identity
{
    public class AspNetUser : IdentityUser<int>
    {
        public virtual Person Person { get; set; }
        public ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
        public ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
    }
}
