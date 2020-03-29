using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data.DAL.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository, IDisposable
    {
        public AddressRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public override void Insert(Address address)
        {
            if (Any(address.AddressID) == true)
            {
                throw new System.InvalidOperationException("This user already has address connected to their account.");
            }
            else
            {
                base.Insert(address);
            }
        }
    }
}