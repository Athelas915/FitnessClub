using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FitnessClub.Data.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Register(IRepository repository);
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        Task Save();
    }
}
