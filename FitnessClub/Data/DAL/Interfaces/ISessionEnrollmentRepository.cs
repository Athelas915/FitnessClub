using FitnessClub.Data.Models;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface ISessionEnrollmentRepository : IGenericRepository<SessionEnrollment>
    {
        void Delete(int PersonID, int SessionID);
        Task<SessionEnrollment> GetByID(int PersonID, int SessionID);
        bool Any(int PersonID, int SessionID);
    }
}
