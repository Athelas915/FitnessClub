using System;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Repositories
{
    public class PersonRepository<PersonEntity> : GenericRepository<PersonEntity>, IPersonRepository<PersonEntity>, IDisposable where PersonEntity : Person

    {
        public PersonRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}