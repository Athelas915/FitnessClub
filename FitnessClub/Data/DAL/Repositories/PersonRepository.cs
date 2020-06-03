using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace FitnessClub.Data.DAL.Repositories
{
    public class PersonRepository<PEntity> : GenericRepository<PEntity>, IPersonRepository<PEntity> where PEntity : Person
    {
        public PersonRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public IEnumerable<PEntity> GetAllWithAddresses() => Get(includeProperties: "Address");
        public PEntity FindWithAddress(int id) => Get(filter: a => a.PersonID == id, includeProperties: "Address").FirstOrDefault();
        public int GetPersonIdByUserId(int userId) => Get(filter: a => a.UserID == userId).FirstOrDefault().PersonID;
    }
    public class PersonRepository : PersonRepository<Person>, IPersonRepository
    {
        public PersonRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
