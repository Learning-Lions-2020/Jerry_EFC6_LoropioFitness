using FitnessApp.Domain.Entities.Base;
using FitnessApp.Domain.Entitities;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data.DBContext;

public class FitnessAppContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<SportActivity> SportActivities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = localhost\\SQLEXPRESS; Database=FitnessDb; Trusted_Connection = True;TrustServerCertificate=True"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(s => s.SportActivities)
            .WithOne(e => e.User)
            .IsRequired();
    }
}