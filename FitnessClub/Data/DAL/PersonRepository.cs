using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data.DAL
{
    public class PersonRepository : IPersonRepository, IDisposable
    {
        private readonly FCContext context;
        public PersonRepository(FCContext context)
        {
            this.context = context;
        }
        
        public IEnumerable<Person> GetPeople()
        {
            return context.People.ToList();
        }
        public Person GetPersonByID(int id)
        {
            return context.People.Find(id);
        }
        public void InsertPerson(Person person)
        {
            context.People.Add(person);
        }
        public void DeletePerson(int personID)
        {
            Person person = context.People.Find(personID);
            context.People.Remove(person);
        }
        public void UpdatePerson(Person person)
        {
            context.Entry(person).State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}