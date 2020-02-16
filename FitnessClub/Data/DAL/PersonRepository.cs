using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
    }
}