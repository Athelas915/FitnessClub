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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FitnessClub.Data.DAL;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FitnessClub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        internal static IConfiguration Configuration { get; private set; }
        public static string CurrentConnectionString { get; private set; } 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (CurrentConnectionString == null)
            {
                CurrentConnectionString = "Development";
            }
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<string>(CurrentConnectionString);

            services.AddRazorPages();

            GetConnectionString.EditJson();
            services.AddDbContext<FCContext>(options => options.UseNpgsql(Configuration.GetConnectionString(CurrentConnectionString)));
            services.AddTransient<IUnitOfWork, UnitOfWork>();


            services.AddIdentity<AspNetUser, AspNetRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<FCContext>()
                .AddUserStore<UserStore<AspNetUser, AspNetRole, FCContext, string, AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetUserToken,AspNetRoleClaim>>()
                .AddRoleStore<RoleStore<AspNetRole, FCContext, string, AspNetUserRole, AspNetRoleClaim>>();

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

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("SignedIn", policy =>
            //        policy.RequireAuthenticatedUser());
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                CurrentConnectionString = "Development";
                app.UseDeveloperExceptionPage();
            }
            else
            {
                CurrentConnectionString = "Production";
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
    }
}
    