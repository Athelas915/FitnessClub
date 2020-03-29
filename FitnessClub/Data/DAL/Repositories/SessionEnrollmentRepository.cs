using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Repositories
{
    //As this entity has composite primary key instead of regular one, the program is overriding some GenericRepository methods so they throw exceptions, forcing user to use pair of IDs instead.
    public class SessionEnrollmentRepository : GenericRepository<SessionEnrollment>, ISessionEnrollmentRepository, IDisposable
    {
        public SessionEnrollmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}