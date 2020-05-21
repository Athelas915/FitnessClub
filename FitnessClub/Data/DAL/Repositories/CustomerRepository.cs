using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
