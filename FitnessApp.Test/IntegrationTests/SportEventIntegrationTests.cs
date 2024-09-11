using FitnessApp.Data.DBContext;
using FitnessApp.Data.Repository;
using FitnessApp.Domain.Entitities;
using FitnessApp.IntegrationTests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FitnessApp.Test.IntegrationTests
{
    public class SportEventIntegrationTests : TestBase
    {
        
        
        [Fact]
        public void AddSportEvent_Should_AddEventToDatabase()
        {
            using (var _context = new FitnessAppContext(Options)) 
            {

                // Arrange
                var repository = new SportEventRepository(_context);
                var sportEvent = new SportEvent
                {
                    Name = "Marathon",
                    Description = "A competitive cycling event.",
                    Date = new DateTime(2024, 10, 20),
                    City = "Los Angeles",
                    Country = "USA"
                };

                // Act
                repository.Save(sportEvent);
                var retrievedEvent = repository.GetSportEventById(sportEvent.Id);

                // Assert
                Assert.NotNull(retrievedEvent);
                Assert.Equal(sportEvent.Name, retrievedEvent.Name);
                      
            }
        }

        [Fact]
        public void GetSportEventById_Should_ReturnCorrectEvent()
        {
            using (var _context = new FitnessAppContext(Options))
            {
                // Arrange
                var repository = new SportEventRepository(_context);
                var sportEvent = new SportEvent
                {
                    Name = "Cycling Race",
                    Description = "A competitive cycling event.",
                    Date = new DateTime(2024, 10, 20),
                    City = "Los Angeles",
                    Country = "USA"
                };
                repository.Save(sportEvent);

                // Act
                var retrievedEvent = repository.GetSportEventById(sportEvent.Id);

                // Assert
                Assert.NotNull(retrievedEvent);
                Assert.Equal(sportEvent.Id, retrievedEvent.Id);
                 
            }
                
        }

        [Fact]
        public void GetAllSportEvents_Should_ReturnAllEvents()
        {
            using (var _context = new FitnessAppContext(Options))
            {
                // Clear all existing sport events to ensure isolation
                _context.SportEvents.RemoveRange(_context.SportEvents);
                _context.SaveChanges();

                // Arrange
                var repository = new SportEventRepository(_context);
                var event1 = new SportEvent
                {
                    Name = "Triathlon",
                    Description = "An endurance event combining swimming, cycling, and running.",
                    Date = new DateTime(2024, 11, 5),
                    City = "San Francisco",
                    Country = "USA"
                };
                var event2 = new SportEvent
                {
                    Name = "Soccer Tournament",
                    Description = "A soccer tournament featuring local teams.",
                    Date = new DateTime(2024, 12, 12),
                    City = "Chicago",
                    Country = "USA"
                };
                repository.Save(event1);
                repository.Save(event2);

                // Act
                var allEvents = repository.GetAllSportEvents();

                // Assert
                Assert.Equal(2, allEvents.Count);
                Assert.Contains(allEvents, e => e.Name == "Triathlon");
                Assert.Contains(allEvents, e => e.Name == "Soccer Tournament");
            }
        }


        [Fact]
        public void AddUserWithSportEvent_Should_UpdateEventWithUsers()
        {
            using (var _context = new FitnessAppContext(Options))
            {
                // Arrange
                var userRepository = new UserRepository(_context);
                var sportEventRepository = new SportEventRepository(_context);
                var user = new User { UserName = "john_doe" };
                userRepository.AddUser(user);

                var sportEvent = new SportEvent
                {
                    Name = "Running Event",
                    Description = "A fun running event.",
                    Date = new DateTime(2024, 9, 30),
                    City = "Seattle",
                    Country = "USA"
                };
                sportEvent.Users.Add(user);
                sportEventRepository.Save(sportEvent);

                // Act
                var retrievedEvent = sportEventRepository.GetSportEventById(sportEvent.Id);

                // Assert
                Assert.NotNull(retrievedEvent);
                Assert.Single(retrievedEvent.Users);
                Assert.Contains(retrievedEvent.Users, u => u.UserName == "john_doe");
            }

        }
    }
}
