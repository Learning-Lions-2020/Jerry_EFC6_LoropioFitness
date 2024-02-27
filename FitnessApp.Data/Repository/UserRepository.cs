using FitnessApp.Data.DBContext;
using FitnessApp.Data.Exceptions;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entities;
using FitnessApp.Domain.Entitities;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data.Repository;

public class UserRepository : IUserRepository
{
    FitnessAppContext _context = new FitnessAppContext();

    public User GetUser(string userName)
    {
        // Task 11: Implement the method to load a user by his userName including his SportActivities

        return _context.Users
        .Include(u => u.SportActivities)
        .SingleOrDefault(u => u.UserName == userName);
    }

    public User AddUser(User user)
    {
        // Task 12: Implement the method to add a user 

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
        // Task 13: Implement the method to load a user by his is including his SportActivities

        return _context.Users
        .Include(u => u.SportActivities)
        .SingleOrDefault(u => u.Id == userId);
    }
}