using FitnessApp.Data;
using FitnessApp.Domain.Activities;
using FitnessApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

public class SportActivity
{
    FitnessAppContext fitnessAppContext = new();
    public User AddUser(string? firstName, string? lastName)
    {
        var user = new User()
        {
            FirstName = firstName,
            LastName = lastName
        };

        return user;
    }

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
        .Include(user => user.BikeActivities)
        .Include(user => user.ClimbActivities)
        .Include(user => user.RunActivities)
        .Include(user => user.SwimActivities)
        .FirstOrDefault(user => user.Id == userId);

        if (user != null)
        {
            Console.WriteLine($"User: {user.FirstName} {user.LastName}");

            // Print Bike Activities
            Console.WriteLine("Bike Activities:");
            if (user.BikeActivities != null && user.BikeActivities.Any())
            {
                foreach (var activity in user.BikeActivities)
                {
                    Console.WriteLine($"  - Id: {activity.Id}, Name: {activity.Name}, Distance: {activity.Distance} km");
                }
            }
            else
            {
                Console.WriteLine("  - Activity not yet recorded");
            }

            // Print Climb Activities
            Console.WriteLine("Climb Activities:");
            if (user.ClimbActivities != null && user.ClimbActivities.Any())
            {
                foreach (var activity in user.ClimbActivities)
                {
                    Console.WriteLine($"  - Id: {activity.Id}, Name: {activity.Name}, Distance: {activity.Distance} meters");
                }
            }
            else
            {
                Console.WriteLine("  - Activity not yet recorded");
            }

            // Print Run Activities
            Console.WriteLine("Run Activities:");
            if (user.RunActivities != null && user.RunActivities.Any())
            {
                foreach (var activity in user.RunActivities)
                {
                    Console.WriteLine($"  - Id: {activity.Id}, Name: {activity.Name}, Distance: {activity.Distance} km");
                }
            }
            else
            {
                Console.WriteLine("  - Activity not yet recorded");
            }

            // Print Swim Activities
            Console.WriteLine("Swim Activities:");
            if (user.SwimActivities != null && user.SwimActivities.Any())
            {
                foreach (var activity in user.SwimActivities)
                {
                    Console.WriteLine($"  - Id: {activity.Id}, Name: {activity.Name}, Distance: {activity.Distance} meters");
                }
            }
            else
            {
                Console.WriteLine("  - Activity not yet recorded");
            }

            Console.WriteLine();
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
            var bikeActivity = user.BikeActivities.FirstOrDefault(a => a.Id == activityId);
            var climbActivity = user.ClimbActivities.FirstOrDefault(a => a.Id == activityId);
            var runActivity = user.RunActivities.FirstOrDefault(a => a.Id == activityId);
            var swimActivity = user.SwimActivities.FirstOrDefault(a => a.Id == activityId);

            if (bikeActivity != null)
            {
                Console.WriteLine($"Bike Activity Details: Name - {bikeActivity.Name}, Distance - {bikeActivity.Distance}");
                return true;
            }
            else if (climbActivity != null)
            {
                Console.WriteLine($"Climb Activity Details: Name - {climbActivity.Name}, Distance - {climbActivity.Distance}");
                return true;
            }
            else if (runActivity != null)
            {
                Console.WriteLine($"Run Activity Details: Name - {runActivity.Name}, Distance - {runActivity.Distance}");
                return true;
            }
            else if (swimActivity != null)
            {
                Console.WriteLine($"Swim Activity Details: Name - {swimActivity.Name}, Distance - {swimActivity.Distance}");
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