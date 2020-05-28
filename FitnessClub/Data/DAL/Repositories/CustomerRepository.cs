using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace FitnessClub.Data.DAL.Repositories
{
    public class CustomerRepository : PersonRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public IEnumerable<Customer> GetAllWithRatings() => Get(includeProperties: "CoachRatings");
        public IEnumerable<Customer> GetAllWithEnrollments() => Get(includeProperties: "SessionEnrollments");
        public IEnumerable<Customer> GetAllWithMemberships() => Get(includeProperties: "Memberships");
        public Customer FindWithMemberships(int id) => Get(filter: a => a.PersonID == id, includeProperties: "Memberships").FirstOrDefault();
        public Customer FindWithEnrollments(int id) => Get(filter: a => a.PersonID == id, includeProperties: "SessionEnrollments").FirstOrDefault();
        public Customer FindWithRatings(int id) => Get(filter: a => a.PersonID == id, includeProperties: "CoachRatings").FirstOrDefault();
    }
}
