using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entitities;
using FitnessApp.Domain.Entitities.Base;
using FitnessApp.Domain.Security;
using Moq;
using Xunit;

namespace FitnessApp.Test
{
    public class UserTests
    {
        [Fact]
        public void Test_GetCredentialsAreValid_Returns_True()
        {
            // Arrange
            var userName = "TestUser";
            var password = "TestPassword";
            var hash = SecurityProvider.HashPasword(password, out var salt);

            var saltForHash = Convert.ToHexString(salt);
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(m => m.GetUser(userName)).Returns(new User() { UserName = userName, PasswordHash = hash, PasswordSalt = saltForHash });

            // Act
            var user = new User(userRepositoryMock.Object);
            var logonValid = user.GetUser(userName, password);

            // Assert
            Assert.True(logonValid != null);
        }

        [Fact]
        public void Test_GetCredentialsAreValid_Returns_False_For_Wrong_Password()
        {
            // Arrange
            var userName = "TestUser";
            var correctPassword = "TestPassword";
            var wrongPassword = "WrongPassword";
            var hash = SecurityProvider.HashPasword(correctPassword, out var salt);
            var saltForHash = Convert.ToHexString(salt);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(m => m.GetUser(userName)).Returns(new User() { UserName = userName, PasswordHash = hash, PasswordSalt = saltForHash });

            // Act
            var user = new User(userRepositoryMock.Object);
            var logonValid = user.GetUser(userName, wrongPassword);

            // Assert
            Assert.False(logonValid != null);
        }
    }
}

