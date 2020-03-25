using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Data.Models.Identity
{
    public class AspNetRoleClaim : IdentityRoleClaim<int>
    {
        [ForeignKey("RoleId")]
        public virtual AspNetRole AspNetRole { get; set; }
        public override int RoleId { get; set; }
    }
}
