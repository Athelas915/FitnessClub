using System;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL.Repositories
{
    public class PersonRepository<PersonEntity> : GenericRepository<PersonEntity>, IPersonRepository<PersonEntity>, IDisposable where PersonEntity : class

    {
        public PersonRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
    }
}