using FitnessApp.Domain.Entities.Base;
using FitnessApp.Domain.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessApp.Data.DBContext;

public class FitnessAppContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<SportActivity> SportActivities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Server = localhost\\SQLEXPRESS; Database=FitnessDb; Trusted_Connection = True;TrustServerCertificate=True")
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
        ;
    }

    // Task 14: in OnModelCreating define the entity relation between User and SportActivity

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.SportActivities)
            .WithOne(a => a.User)
            .IsRequired();
    }

}