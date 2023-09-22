using FitnessApp.Data;
using FitnessApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

public class SportActivity
{
    FitnessAppContext fitnessAppContext = new();

    public User? FindUserById(int userId)
    {
        return fitnessAppContext.Users.Find(userId);
    }

    public void PrintUserId(int id)
    {
        User? user = FindUserById(id);

        if (user != null)
        {
            Console.WriteLine("Found User!");
            Console.WriteLine($"User Details: \nUserId: {user.Id}, First Name: {user.FirstName}, " +
                $"Last Name: {user.LastName}\n");
        }
        else
        {
            Console.WriteLine($"User with UserId {id} not found.");
        }
    }

    public void GetUserAndActivities(int userId)
    {
        var user = fitnessAppContext.Users
            .Include(user => user.RunActivities)
            .FirstOrDefault(user => user.Id == userId);

        if (user != null)
        {
            Console.WriteLine($"First Name: {user.FirstName}, Last Name: {user.LastName}");
            foreach (var activity in user.RunActivities)
            {
                Console.WriteLine($"Name of Activity: {activity.Name} Distance: {activity.Distance}");
            }
        }
        else
        {
            Console.WriteLine("User not found or no activities recorded yet.");
        }
    }

    public void UpdateFirstName(int userId, string? toFirstName)
    {
        User? user = fitnessAppContext.Users.FirstOrDefault(u => u.Id == userId);

        if (user != null)
        {
            Console.WriteLine($"User Details before update: FirstName: {user.FirstName}");
            if (toFirstName != null) user.FirstName = toFirstName;

            Console.WriteLine("Before calling DetectChanges: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);
            fitnessAppContext.ChangeTracker.DetectChanges();
            Console.WriteLine("After calling DetectChanges: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);

            fitnessAppContext.SaveChanges();

            Console.WriteLine($"User Details After Update: FirstName: {user.FirstName}");
        }
        else
        {
            Console.WriteLine("User not found for updating FirstName.");
        }
    }

    public void UpdateLastName(int userId, string? toLastName)
    {
        User? user = fitnessAppContext.Users.FirstOrDefault(u => u.Id == userId);

        if (user != null)
        {
            Console.WriteLine($"User Details before update: Last Name: {user.LastName}");
            if (toLastName != null) user.LastName = toLastName;

            Console.WriteLine("Before calling DetectChanges: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);
            fitnessAppContext.ChangeTracker.DetectChanges();
            Console.WriteLine("After calling DetectChanges: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);

            fitnessAppContext.SaveChanges();

            Console.WriteLine($"User Details After Update: Last Name: {user.LastName}");
        }
        else
        {
            Console.WriteLine("User not found for updating Last Name.");
        }
    }
}