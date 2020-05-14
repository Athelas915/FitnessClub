using FitnessClub.Data.Models;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IPersonRepository<PersonEntity> : IGenericRepository<PersonEntity> where PersonEntity : Person
    {
        new void Insert(PersonEntity person);
    }
}