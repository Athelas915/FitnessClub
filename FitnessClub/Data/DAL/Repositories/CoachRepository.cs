using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Repositories
{
    public class CoachRepository : PersonRepository<Coach>, ICoachRepository
    {
        public CoachRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
