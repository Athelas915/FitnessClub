using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        ISessionRepository SessionRepository { get; }
        ICoachRepository CoachRepository { get; }
        Task Commit();
    }
}
