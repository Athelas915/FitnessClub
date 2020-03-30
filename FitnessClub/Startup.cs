using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Repositories;
using FitnessClub.Data.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FitnessClub
{
    public class Startup
    {
        internal static IConfiguration Configuration { get; private set; }
        public static string CurrentConnString { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                CurrentConnString = Configuration.GetConnectionString("FCContext");
            }
            else
            {
                CurrentConnString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_FCContext");
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            //CurrentConnString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_FCContext");
            //CurrentConnString = Configuration.GetConnectionString("FCContext");
            //CurrentConnString = Configuration.GetConnectionString("POSTGRESQLCONNSTR_FCContext");
            services.AddSingleton<string>(CurrentConnString);

            services.AddDbContext<FCContext>(options => options.UseNpgsql(CurrentConnString));

            RegisterRepositories(services); //this function keeps the code cleaner: there are many repositories to register, so they are stored in separate class.

            services.AddIdentity<AspNetUser, AspNetRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<FCContext>()
                .AddUserStore<UserStore<AspNetUser, AspNetRole, FCContext, int, AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetUserToken,AspNetRoleClaim>>()
                .AddRoleStore<RoleStore<AspNetRole, FCContext, int, AspNetUserRole, AspNetRoleClaim>>();
            


            //services.AddDefaultIdentity<AspNetUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<FCContext>();



            services.AddAuthentication()
            .AddGoogle(options =>   
            {
                IConfigurationSection googleAuthNSection =
                    Configuration.GetSection("Authentication:Google");

                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SignedIn", policy =>
                    policy.RequireAuthenticatedUser());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
        private static void RegisterRepositories(IServiceCollection services)
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
        }
    }
}
    