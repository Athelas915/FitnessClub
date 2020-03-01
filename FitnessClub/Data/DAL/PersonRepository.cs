using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data.DAL
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository, IDisposable

    {
        public PersonRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
    }
}