using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository, IDisposable
    {
        public AddressRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
    }
}