using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL
{
    public class PersonRepository : IPersonRepository, IDisposable
    {
        private readonly FCContext context;
        public PersonRepository(FCContext context)
        {
            this.context = context;
        }
        
        public async Task<IList<Person>> GetPeople()
        {
            return await context.People.ToListAsync();
        }
        public async Task<Person> GetPersonByID(int id)
        {
            return await context.People.FindAsync(id);
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
        public async Task Save()
        {
            await context.SaveChangesAsync();
            return;
        }

        public bool Any(int id)
        {
            return context.People.Any(e => e.PersonID == id);
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