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
            => optionsBuilder.UseNpgsql("Host=ec2-54-246-90-10.eu-west-1.compute.amazonaws.com;Database=df8btp2v90h9t1;Username=otnzwcbaeerohs;Password=ab83af16a06c11102cc57b387af7e936c7e0cc5f4c21a41501955cb59e10f09e;SSL Mode=Require;Trust Server Certificate=true");
    }
}
