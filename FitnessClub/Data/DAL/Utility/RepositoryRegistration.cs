using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Repositories;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Utility
{
    public static class RepositoryRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IPersonRepository<Coach>, PersonRepository<Coach>>();
            services.AddScoped<ICoachRatingRepository, CoachRatingRepository>();
            services.AddScoped<IPersonRepository<Customer>, PersonRepository<Customer>>();
            services.AddScoped<IPersonRepository<Employee>, PersonRepository<Employee>>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IPersonRepository<Person>, PersonRepository<Person>>();
            services.AddScoped<ISessionEnrollmentRepository, SessionEnrollmentRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
        }
    }
}
