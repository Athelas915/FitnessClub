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
            if (unitOfWork.LoggedUserId == -1)
            {
                address.CreatedBy = address.Person.AspNetUser.Id;
                dbSet.Add(address);
            }
            else
            {
                base.Insert(address);
            }
        }
    }
}