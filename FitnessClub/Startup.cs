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
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using Serilog.Settings.Configuration;
using Microsoft.AspNetCore.Http;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Repositories;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.BLL.Services;
using FitnessClub.Data.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.Extensions.Options;

namespace FitnessClub
{
    public class Startup
    {
        internal static IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.AddConfiguration(configuration);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            //next part sets the configuration setting ConnectionString depending on which environment is currently on.
            if (env.EnvironmentName == "Development")
            {
                //In development, the connection string is taken from secrets.json file that isn't checked in at github.
                Configuration["ConnectionStrings:FCConnectionString"] = Configuration.GetConnectionString("FCContextDevelopment");
            }
            else
            {
                //In production, the connection string is taken from environment variable set during app deployment.
                Configuration["ConnectionStrings:FCConnectionString"] = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_FCContext");
                Configuration["DefaultAdminPassword"] = Environment.GetEnvironmentVariable("DefaultPassword");
            }
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddDbContext<FCContext>(options => options.UseNpgsql(Configuration.GetConnectionString("FCConnectionString")));


        services.AddIdentity<AspNetUser, AspNetRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
            })
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<FCContext>()
                .AddUserStore<UserStoreFC>()
                .AddRoleStore<RoleStore<AspNetRole, FCContext, int, AspNetUserRole, AspNetRoleClaim>>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICoachRepository, CoachRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            services.AddScoped<ICoachRatingService, CoachRatingService>();
            services.AddScoped<IAccountManagementService, AccountManagementService>();

            services.AddTransient<UserResolverService>();

            services.AddAuthentication()
            .AddGoogle(options =>   
            {
                IConfigurationSection googleAuthNSection =
                    Configuration.GetSection("Authentication:Google");

                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });
            
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AspNetUser> userManager, RoleManager<AspNetRole> roleManager)
        {
            //Serilog settings for different environments. They're kept here instead of "appsettings.json" because of issues with that method:
            //1. New version of serilog package can't handle parsing {date} when used inside json;
            //2. Connection string for Serilog.Sinks.PostgreSQL in appsettings needs to be stored as full string, and the package doesn't support using 
            //string's name given during configuration.If the app run on MS SQL, it would have been possible.
            //Other workarounds has been tested and keeping them here appeared to be the cleanest option.
            if (env.IsDevelopment())
            {
                Serilog.Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.File(
                path: $"LogFiles/log-{DateTime.Now.ToString("MM/dd/yyyy")}.txt",
                fileSizeLimitBytes: 10485760,
                rollOnFileSizeLimit: true,
                retainedFileCountLimit: null
                )
                .CreateLogger();

                app.UseDeveloperExceptionPage();
            }
            else
            {
                Serilog.Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Error()
                .WriteTo.PostgreSQL(
                connectionString: Configuration.GetConnectionString("FCConnectionString"),
                tableName: "logs",
                needAutoCreateTable: false,
                batchSizeLimit: 1
                )
                .CreateLogger();

                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            IdentityDataInitializer.SeedData(userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
        //This class is supposed to configure UserStore type. AutoSaveChanges = false means that operations on UserManager (such as AddUser) don't automatically commit changes to the context.
        //Since I'm using repository pattern with unit of work, I want my UserManager changes to save at the same time with other repositories. This configuration enables such behavior.
        //Also, the this long string of types in UserStore<> won't clog the Startup.ConfigureServices.
        public class UserStoreFC : UserStore<AspNetUser, AspNetRole, FCContext, int, AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetUserToken, AspNetRoleClaim>, IUserStore<AspNetUser>
        {
            public UserStoreFC(FCContext context) : base(context)
            {
                AutoSaveChanges = false;
            }
        }
    }
}
    
