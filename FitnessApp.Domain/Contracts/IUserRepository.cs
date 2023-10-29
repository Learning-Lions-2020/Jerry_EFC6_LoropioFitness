using FitnessApp.Domain.Entitities;

namespace FitnessApp.Domain.Contracts
{
    public interface IUserRepository
    {
        User GetUser(string userName);
        public User Save(User user);
    }
}
