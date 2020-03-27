using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL.Repositories
{
    public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository, IDisposable
    {
        public HolidayRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}