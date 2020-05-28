using FitnessClub.Data.Models;
using System.Collections.Generic;

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
