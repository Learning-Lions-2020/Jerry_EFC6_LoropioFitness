using FitnessApp.Domain;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entitities;
using Moq;
using Xunit;

namespace FitnessApp.Test
{
    public class UserTests
    {
        [Fact]
        public void Test_GetCredentialsAreValid_Returns_True()
        {
            //arrange
            var userName = "TestUser";
            var password = "TestPassword";
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(m => m.GetUser(userName)).Returns(new User() {UserName = userName});
            //act
            var user = new User(userRepositoryMock.Object);
            var logonValid = user.GetCredentialsAreValid(userName, password);
            //assert
            Assert.True(logonValid);
        }
    }
}