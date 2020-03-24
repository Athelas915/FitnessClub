using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL.Repositories
{
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository, IDisposable
    {
        public MembershipRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
    }
}