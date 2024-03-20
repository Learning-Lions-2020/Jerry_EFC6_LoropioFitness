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

        var user = _context.Users
        .Include(u => u.SportActivities)
        .FirstOrDefault(u => u.UserName == userName);

        if (user != null)
        {
            return user;
        }
        throw new Exception($"User with the userName: {userName} not found!");

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


        var user = _context.Users
        .Include(u => u.SportActivities)
        .FirstOrDefault(u => u.Id == userId);

        if (user != null)
        {
            return user;
        }
        return null;

    }
}