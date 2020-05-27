using FitnessClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IPersonRepository<PEntity> : IRepository<PEntity> where PEntity : Person
    {
        IEnumerable<PEntity> GetAllWithAddresses();
        PEntity FindWithAddress(int id);
    }
}
