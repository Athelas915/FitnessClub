using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Data.Models.Identity  
{
    public class AspNetUserRole : IdentityUserRole<int>
    {
        [ForeignKey("UserId")]
        public virtual AspNetUser AspNetUser { get; set; }
        [ForeignKey("RoleId")]
        public virtual AspNetRole AspNetRole { get; set; }
    }
}
