using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL.Repositories
{
    public class CoachRatingRepository : GenericRepository<CoachRating>, ICoachRatingRepository, IDisposable
    {
        public CoachRatingRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}