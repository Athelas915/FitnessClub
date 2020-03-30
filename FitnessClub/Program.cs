﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FitnessClub.Data.DAL;
using FitnessClub.Pages.DataManagement.People;

namespace FitnessClub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Startup.CurrentConnString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_FCContext");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
