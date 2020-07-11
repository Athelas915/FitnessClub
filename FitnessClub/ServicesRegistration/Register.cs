using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.BLL.Services;
using FitnessClub.Data.DAL;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.ServicesRegistration
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICoachRepository, CoachRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            return services;
        }
        public static IServiceCollection RegisterBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<ISessionManagementService, SessionManagementService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}
