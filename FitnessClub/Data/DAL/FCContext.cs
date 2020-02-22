using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data.DAL
{
    public class FCContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Adress> Adresses { get; set; }
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
            => optionsBuilder.UseNpgsql(GetConnectionString.ConvertDbURL());
    }
}
