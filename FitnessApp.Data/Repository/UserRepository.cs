using FitnessApp.Data.DBContext;
using FitnessApp.Data.Exceptions;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        FitnessAppContext _context = new FitnessAppContext();

        public User GetUser(string userName)
        {
            var user = _context.Users.Include(u=> u.SportActivities).FirstOrDefault(u => u.UserName == userName);
            if (user != null) return user;
             throw new UserNotFoundException($"User with Username: {userName} does not exist");
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void SaveOrUpdate()
        {
            _context.SaveChanges();
        }

        public User? GetUserById(int userId)
        {
            var user = _context.Users.Include(u => u.SportActivities).FirstOrDefault(u => u.Id == userId);
            if (user != null) return user;
            return null;
        }
    }
}
