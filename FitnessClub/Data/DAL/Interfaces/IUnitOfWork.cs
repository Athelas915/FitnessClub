using System.Threading.Tasks;
using System.Collections.Generic;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public int LoggedUserId { get; }
        public FCContext Context { get; }
        Task Save();
    }
}
