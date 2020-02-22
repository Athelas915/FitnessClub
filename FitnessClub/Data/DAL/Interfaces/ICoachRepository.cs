using System;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface ICoachRepository : IGenericRepository<Coach>, IDisposable
    {
    }
}