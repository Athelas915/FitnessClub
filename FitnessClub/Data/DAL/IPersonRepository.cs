using System;
using System.Collections.Generic;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL
{
    public interface IPersonRepository : IDisposable
    {
        IEnumerable<Person> GetPeople();
        Person GetPersonByID(int personID);
        void InsertPerson(Person person);
        void DeletePerson(int personID);
        void UpdatePerson(Person person);
        void Save();
    }
}