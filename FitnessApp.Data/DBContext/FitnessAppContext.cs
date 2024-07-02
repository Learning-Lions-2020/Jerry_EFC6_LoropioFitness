using FitnessApp.Domain.Entities.Base;
using FitnessApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Domain.Entitities.Base;
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
        public DbSet<UserSportEvent> UserSportEvents { get; set; } 


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=FitnessDb;Trusted_Connection=True;TrustServerCertificate=True")
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.SportActivities)
                .WithOne(a => a.User)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.RegisteredEvents)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<SportEvent>()
                .HasKey(se => se.Id);

            modelBuilder.Entity<SensorData>()
                .HasKey(sd => sd.Id);

            modelBuilder.Entity<SensorData>()
                .HasOne(sd => sd.SportActivity)
                .WithMany(sa => sa.SensorDatas)
                .HasForeignKey(sd => sd.SportActivityId);


            modelBuilder.Entity<UserSportEvent>()
                .HasKey(ue => new { ue.UserId, ue.SportEventId });

            modelBuilder.Entity<UserSportEvent>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.UserSportEvents)
                .HasForeignKey(ue => ue.UserId);

            modelBuilder.Entity<UserSportEvent>()
                .HasOne(ue => ue.SportEvent)
                .WithMany(se => se.UserSportEvents)
                .HasForeignKey(ue => ue.SportEventId);
        }
    }
}



