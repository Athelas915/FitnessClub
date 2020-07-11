using FitnessClub.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IPersonRepository<PEntity> : IRepository<PEntity> where PEntity : Person
    {
    }
    public interface IPersonRepository : IPersonRepository<Person>
    {

    }
}
