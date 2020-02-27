using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data.DAL
{
    public class PersonRepository<PersonEntity> : GenericRepository<PersonEntity>, IPersonRepository<PersonEntity>, IDisposable where PersonEntity : class

    {
        public PersonRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
    }
}