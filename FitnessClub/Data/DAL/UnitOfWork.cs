using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FCContext context;
        private readonly Dictionary<string, IRepository> repositories;
        public UnitOfWork(FCContext context)
        {
            this.context = context;
            repositories = new Dictionary<string, IRepository>();
        }
        public void Register(IRepository repository)
        {
            repositories.Add(repository.GetType().Name, repository);
        }
        public DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return context.Set<TEntity>();
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransaction() => await context.Database.BeginTransactionAsync();

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
