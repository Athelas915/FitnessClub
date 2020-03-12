using System;
using System.Linq;
using FitnessClub.Data.Models;
using FitnessClub;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL
{
    public class FCContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<CoachRating> CoachRatings { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<SessionEnrollment> SessionEnrollments { get; set; }
        public FCContext(DbContextOptions<FCContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseNpgsql(Startup.Configuration.GetConnectionString("FCContext"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model
                .GetEntityTypes()
                .Where(w => w.ClrType.IsSubclassOf(typeof(BaseEntity)))
                .Select(c => modelBuilder.Entity(c.ClrType)))
            {
                entity
                    .Property("CreatedOn")
                    .HasDefaultValueSql("now()")
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                entity
                    .Property("CreatedBy")
                    .HasDefaultValue(0)
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            }
        }
    }
}
