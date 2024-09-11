using FitnessApp.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FitnessApp.IntegrationTests
{
    public  class TestBase 
    {
        protected DbContextOptions<FitnessAppContext> Options { get; set; } 

        protected TestBase()
        {
            var uiProjectDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..","..", "..", "FitnessApp.API");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(uiProjectDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Options = new DbContextOptionsBuilder<FitnessAppContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
    }
}

