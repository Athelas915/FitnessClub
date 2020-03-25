using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FitnessClub.Data.Models.Identity
{
    public class AspNetRole : IdentityRole
    {
        public ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    }
}
