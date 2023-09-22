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

    public bool PrintOneActivityByItsId(int userId, int activityId)
    {
        User? user = FindUserById(userId);

        if (user != null)
        {
            var activity = user.RunActivities.FirstOrDefault(a => a.Id == activityId);

            if (activity != null)
            {
                Console.WriteLine($"Activity Details: Name - {activity.Name}, Distance - {activity.Distance}");
                return true;
            }
        }

        Console.WriteLine($"Activity with ID {activityId} not found for user ID {userId}");
        return false;
    }

    public void UpdateActivityName(int userId, int activityId, string? newActivityName)
    {
        User? user = fitnessAppContext.Users
            .Include(u => u.RunActivities)
            .FirstOrDefault(u => u.Id == userId);

        if (user != null)
        {
            var activity = user.RunActivities.FirstOrDefault(a => a.Id == activityId);

            if (activity != null)
            {
                Console.WriteLine($"Activity Name before update: {activity.Name}");
                if (newActivityName != null) activity.Name = newActivityName;

                Console.WriteLine("Before calling DetectChanges: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);
                fitnessAppContext.ChangeTracker.DetectChanges();
                Console.WriteLine("After calling DetectChanges: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);

                fitnessAppContext.SaveChanges();

                Console.WriteLine($"Activity Name After Update: {activity.Name}");
            }
            else
            {
                Console.WriteLine($"Activity with ID {activityId} not found for user with ID {userId}.");
            }
        }
        else
        {
            Console.WriteLine($"User with ID {userId} not found.");
        }
    }

    public void UpdateActivityDistance(int userId, int activityId, string? newActivityDistance)
    {
        User? user = fitnessAppContext.Users
            .Include(u => u.RunActivities)
            .FirstOrDefault(u => u.Id == userId);

        if (user != null)
        {
            var activity = user.RunActivities.FirstOrDefault(a => a.Id == activityId);

            if (activity != null)
            {
                Console.WriteLine($"Activity Distance before update: {activity.Distance}");

                if (double.TryParse(newActivityDistance, out double parsedDistance))
                {
                    activity.Distance = parsedDistance;

                    Console.WriteLine("Before calling DetectChanges: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);
                    fitnessAppContext.ChangeTracker.DetectChanges();
                    Console.WriteLine("After calling DetectChanges: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);

                    fitnessAppContext.SaveChanges();

                    Console.WriteLine($"Activity Distance After Update: {activity.Distance}");
                }
                else
                {
                    Console.WriteLine("Invalid input for distance. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine($"Activity with ID {activityId} not found for user with ID {userId}.");
            }
        }
        else
        {
            Console.WriteLine($"User with ID {userId} not found.");
        }
    }

}