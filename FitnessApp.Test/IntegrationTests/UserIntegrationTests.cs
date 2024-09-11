using FitnessApp.Data.DBContext;
using FitnessApp.Data.Repository;
using FitnessApp.Domain.CustomTypes;
using FitnessApp.Domain.Entities.Base;
using FitnessApp.Domain.Entitities;
using FitnessApp.IntegrationTests;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FitnessApp.Test.IntegrationTests
{
    public class UserIntegrationTests : TestBase
    {
        [Fact]
        public void GetUser_AddOneUser_ListIsNotEmpty()
        {
            using (var _context = new FitnessAppContext(Options))
            {
                // Arrange
                var repository = new UserRepository(_context);
                var user = new User { UserName = "testUser" };

                // Act
                repository.AddUser(user);
                var retrievedUser = repository.GetUser(user.UserName);

                // Assert
                Assert.NotNull(retrievedUser);
                Assert.Equal("testUser", retrievedUser.UserName);
            }
        }

        [Fact]
        public void GetUser_AddOneUserWithSportActivity_ActivityListIsNotEmpty()
        {
            using (var _context = new FitnessAppContext(Options))
            {
                // Arrange
                var repository = new UserRepository(_context);
                var user = new User
                {
                    UserName = "userWithActivity",
                    SportActivities = new List<SportActivity>
                    {
                        new SportActivity
                        {
                            Id = Guid.NewGuid(),
                            Distance = 10.0,
                            TimeTaken = TimeSpan.FromHours(1),
                            ActivityDate = DateTime.Now,
                            ActivityType = ActivityType.BikeActivity,
                            Feeling = Feeling.Strong
                        }
                    }
                };

                // Act
                repository.AddUser(user);
                var retrievedUser = repository.GetUser(user.UserName);

                // Assert
                Assert.NotNull(retrievedUser);
                Assert.NotNull(retrievedUser.SportActivities);
                Assert.Single(retrievedUser.SportActivities);
            }
        }

        [Fact]
        public void GetUserById_AddOneUserWithSportActivity_ActivityListIsNotEmpty()
        {
            using (var _context = new FitnessAppContext(Options))
            {
                // Arrange
                var repository = new UserRepository(_context);
                var user = new User
                {
                    UserName = "userWithActivityById",
                    SportActivities = new List<SportActivity>
                    {
                        new SportActivity
                        {
                            Id = Guid.NewGuid(),
                            Distance = 5.0,
                            TimeTaken = TimeSpan.FromMinutes(30),
                            ActivityDate = DateTime.Now,
                            ActivityType = ActivityType.RunActivity,
                            Feeling = Feeling.Good
                        }
                    }
                };

                // Act
                repository.AddUser(user);
                var retrievedUser = repository.GetUserById(user.Id);

                // Assert
                Assert.NotNull(retrievedUser);
                Assert.NotNull(retrievedUser.SportActivities);
                Assert.Single(retrievedUser.SportActivities);
            }
        }

        [Fact]
        public void GetUserById_AddOneUserWithoutSportActivity_ActivityListEmpty()
        {
            using (var _context = new FitnessAppContext(Options))
            {
                // Arrange
                var repository = new UserRepository(_context);
                var user = new User { UserName = "userWithoutActivity" };

                // Act
                repository.AddUser(user);
                var retrievedUser = repository.GetUserById(user.Id);

                // Assert
                Assert.NotNull(retrievedUser);
                Assert.NotNull(retrievedUser.SportActivities);
                Assert.Empty(retrievedUser.SportActivities);
            }
        }

        [Fact]
        public void RemoveAllActivities_RemovesActivities()
        { 
            using (var _context = new FitnessAppContext(Options))
            {
                // Arrange
                var repository = new UserRepository(_context);
                var user = new User
                {
                    UserName = "testUser",
                    SportActivities = new List<SportActivity>
                    {
                        new SportActivity { Distance = 5.0, ActivityDate = DateTime.Now }
                    }
                };
                repository.AddUser(user);

                // Act
                repository.RemoveAllActivities(user.Id);
                var retrievedUser = repository.GetUserById(user.Id);

                // Assert
                Assert.NotNull(retrievedUser);
                Assert.Empty(retrievedUser.SportActivities);
            }
        }
    }
}
