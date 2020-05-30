using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Register(IRepository repository);
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        Task Save();
        Task<IDbContextTransaction> BeginTransaction();
    }
}
