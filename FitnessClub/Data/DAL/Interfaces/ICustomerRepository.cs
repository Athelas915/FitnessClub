using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface ICustomerRepository : IPersonRepository<Customer>
    {
        IEnumerable<Customer> GetAllWithMemberships();
        IEnumerable<Customer> GetAllWithEnrollments();
        IEnumerable<Customer> GetAllWithRatings();
        Customer FindWithMemberships(int id);
        Customer FindWithEnrollments(int id);
        Customer FindWithRatings(int id);

    }
}
