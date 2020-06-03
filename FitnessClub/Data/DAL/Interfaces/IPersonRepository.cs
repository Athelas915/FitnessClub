using FitnessClub.Data.Models;
using System.Collections.Generic;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IPersonRepository<PEntity> : IRepository<PEntity> where PEntity : Person
    {
        IEnumerable<PEntity> GetAllWithAddresses();
        PEntity FindWithAddress(int id);
        int GetPersonIdByUserId(int userId);
    }
    public interface IPersonRepository : IPersonRepository<Person>
    {

    }
}
