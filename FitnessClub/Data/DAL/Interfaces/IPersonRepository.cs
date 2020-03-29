using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IPersonRepository<PersonEntity> : IGenericRepository<PersonEntity> where PersonEntity : class
    {
    }
}