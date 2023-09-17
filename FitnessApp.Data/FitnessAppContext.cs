using Microsoft.EntityFrameworkCore;
using FitnessApp.Domain;


namespace FitnessApp.Data
{
    public class FitnessAppContext : DbContext
    {
        public DbSet<RunActivity> RunActivities { get; set; }
        public DbSet<User> Users { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = localhost\\SQLEXPRESS; Database=FitnessDb; Trusted_Connection = True;TrustServerCertificate=True"
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            var userList = new List<User>()
            {
                new User() { Id = 1, FirstName = "Bonface", LastName = "Njuguna"},
                new User() {  Id = 2, FirstName = "Omondi", LastName = "Wyclife"},
                new User() {Id = 3,  FirstName = "Tabby", LastName = "Ayako"},
                new User() { Id = 4, FirstName = "Isaya", LastName = "Mutekhele"}
            };
            modelBuilder.Entity<User>().HasData(userList);
        }
    }
}
