using System;
using System.Collections.Generic;
using FitnessClub.Data.Models;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL
{
    public interface IPersonRepository : IDisposable
    {
        Task<IList<Person>> GetPeople();
        Task<Person> GetPersonByID(int personID);
        void InsertPerson(Person person);
        void DeletePerson(int personID);
        void UpdatePerson(Person person);
        Task Save();
        bool Any(int id);
    }
}