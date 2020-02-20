using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository<Person> PersonRepository { get; }
        ISessionRepository SessionRepository { get; }
        public void Save();
    }
}
