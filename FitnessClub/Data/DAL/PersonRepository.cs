using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL
{
    public class PersonRepository<PersonEntity> : GenericRepository<PersonEntity>, IPersonRepository<PersonEntity> where PersonEntity : class
    {
        public PersonRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
    }
}