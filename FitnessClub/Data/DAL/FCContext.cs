using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace FitnessClub.Data.DAL
{
    public class FCContext : IdentityDbContext<AspNetUser, AspNetRole, int>
    {
        //App identity and role storage data

        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

        //Business usage data
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
        //Logging into database table
        public DbSet<Log> Logs { get; set; }
        public FCContext(DbContextOptions<FCContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseNpgsql(Startup.Configuration.GetConnectionString("FCConnectionString"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model
                .GetEntityTypes()
                .Where(w => w.ClrType.IsSubclassOf(typeof(DataEntity)))
                .Select(c => modelBuilder.Entity(c.ClrType)))
            {

                entity
                    .Property("CreatedOn")
                    .HasDefaultValueSql("now()")
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                entity
                    .Property("CreatedBy")
                    .HasDefaultValueSql("-1")
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            }

            modelBuilder.Entity<AspNetUser>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<SessionEnrollment>()
                .HasKey(o => new { o.CustomerID, o.SessionID });
            modelBuilder.Entity<Log>().ToTable("logs");

            modelBuilder.Entity<AspNetUser>()
                .HasOne(a => a.Person)
                .WithOne(b => b.AspNetUser)
                .HasForeignKey<Person>(b => b.UserID)
                .IsRequired();

            modelBuilder.Entity<Person>()
                .HasOne(a => a.Address)
                .WithOne(b => b.Person)
                .HasForeignKey<Address>(b => b.PersonID)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasMany(a => a.Memberships)
                .WithOne(b => b.Customer)
                .HasForeignKey(b => b.CustomerID)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasMany(a => a.SessionEnrollments)
                .WithOne(b => b.Customer)
                .HasForeignKey(b => b.CustomerID)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasMany(a => a.CoachRatings)
                .WithOne(b => b.Customer)
                .HasForeignKey(b => b.CustomerID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Coach>()
                .HasMany(a => a.CoachRatings)
                .WithOne(b => b.Coach)
                .HasForeignKey(b => b.CoachID)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .HasMany(a => a.Holidays)
                .WithOne(b => b.Employee)
                .HasForeignKey(b => b.EmployeeID)
                .IsRequired();

            modelBuilder.Entity<Coach>()
                .HasMany(a => a.Sessions)
                .WithOne(b => b.Coach)
                .HasForeignKey(b => b.CoachID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Session>()
                .HasMany(a => a.SessionEnrollments)
                .WithOne(b => b.Session)
                .HasForeignKey(b => b.SessionID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
