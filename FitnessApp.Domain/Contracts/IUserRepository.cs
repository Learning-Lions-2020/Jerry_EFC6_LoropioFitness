using FitnessApp.Domain.Entities;

namespace FitnessApp.Domain.Contracts
{
    public interface IUserRepository
    {
        User GetUser(string userName);
        public User AddUser(User user);
        public void SaveOrUpdate();
        public User? GetUserById(int userId);
    }
}
