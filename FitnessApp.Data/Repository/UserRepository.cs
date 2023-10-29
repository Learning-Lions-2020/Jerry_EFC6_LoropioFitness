using FitnessApp.Data.DBContext;
using FitnessApp.Data.Exceptions;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entitities;

namespace FitnessApp.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        FitnessAppContext _context = new FitnessAppContext();

        public User GetUser(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user != null) return user;
            throw new UserNotFoundException($"User with Username: {userName} does not exist");
        }

        public User Save(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

    }
}
