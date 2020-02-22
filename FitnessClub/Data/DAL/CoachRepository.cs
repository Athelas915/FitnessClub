using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data.DAL
{
    public class CoachRepository : GenericRepository<Coach>, ICoachRepository, IDisposable
    {
        public CoachRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
    }
}