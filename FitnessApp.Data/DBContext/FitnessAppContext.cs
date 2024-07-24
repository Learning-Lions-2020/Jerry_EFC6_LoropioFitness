using FitnessApp.Domain.Entities.Base;
using FitnessApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Domain.Entitities;
using Microsoft.Extensions.Logging;

namespace FitnessApp.Data.DBContext
{
    public class FitnessAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SportActivity> SportActivities { get; set; }
        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<SensorData> SensorDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=FitnessDbNew;Trusted_Connection=True;TrustServerCertificate=True");
                /*.LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();*/
        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.SportActivities)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<SportActivity>()
                .HasOne(a => a.SensorData)
                .WithOne(b => b.SportActivity)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<SensorData>()
                .IsRequired(false);
        }
    }
}



