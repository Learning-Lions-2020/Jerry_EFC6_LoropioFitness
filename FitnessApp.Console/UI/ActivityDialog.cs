using FitnessApp.Console.Activity;
using FitnessApp.Data;
using FitnessApp.Domain.Activities;
using FitnessApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

public class ActivityDialog
{
    private static ActivityType[] validActivities = { ActivityType.BikeActivity, ActivityType.ClimbActivity, ActivityType.RunActivity, ActivityType.SwimActivity };

    internal static void AddUserWithActivities()
    {
        SportActivity sportActivity = new SportActivity();

        Console.WriteLine("Enter First Name: ");
        string? firstName = Console.ReadLine();

        Console.WriteLine("Enter Last Name: ");
        string? lastName = Console.ReadLine();

        User newUser = sportActivity.AddUser(firstName, lastName);

        Console.WriteLine("What type of activity do you want to enter?");
        Console.WriteLine("1. Bike Activity\n2. Climb Activity\n3. Run Activity\n4. Swim Activity");
        Console.Write("Your selection: ");

        string? activityTypeInput = Console.ReadLine();

        if (int.TryParse(activityTypeInput, out int selectedActivityIndex) && selectedActivityIndex >= 1 && selectedActivityIndex <= validActivities.Length)
        {
            ActivityType activityType = validActivities[selectedActivityIndex - 1];
            OpenActivityDialog(newUser, activityType);
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    private static void OpenActivityDialog(User user, ActivityType activityType)
    {
        switch (activityType)
        {
            case ActivityType.BikeActivity:
                AddBikeActivity(user);
                break;
            case ActivityType.ClimbActivity:
                AddClimbActivity(user);
                break;
            case ActivityType.RunActivity:
                AddRunActivity(user);
                break;
            case ActivityType.SwimActivity:
                AddSwimActivity(user);
                break;
            default:
                Console.WriteLine("Invalid SportActivity");
                break;
        }
    }

    private static void AddBikeActivity(User user)
    {
        Console.Write("Name of the bike activity: ");
        string? activityNameInput = Console.ReadLine();

        Console.Write("Enter distance in km: ");
        string? distanceInput = Console.ReadLine();

        if (double.TryParse(distanceInput, out double distance) && distance >= 0)
        {
            using (var context = new FitnessAppContext())
            {
                var bikeActivity = new BikeActivity
                {
                    Name = activityNameInput,
                    Distance = distance,
                };

                user.BikeActivities.Add(bikeActivity);
                context.Users.Update(user);

                context.SaveChanges();
                Console.WriteLine("Added User and Bike Activity successfully!");
            }
        }
        else
        {
            Console.WriteLine("Invalid distance format!");
        }
    }

    private static void AddClimbActivity(User user)
    {
        Console.Write("Name of the climb activity: ");
        string? activityNameInput = Console.ReadLine();

        Console.Write("Enter distance in meters: ");
        string? distanceInput = Console.ReadLine();

        if (double.TryParse(distanceInput, out double distance) && distance >= 0)
        {
            using (var context = new FitnessAppContext())
            {
                var climbActivity = new ClimbActivity
                {
                    Name = activityNameInput,
                    Distance = distance,
                };

                user.ClimbActivities.Add(climbActivity);
                context.Users.Update(user);

                context.SaveChanges();
                Console.WriteLine("Added User and Climb Activity successfully!");
            }
        }
        else
        {
            Console.WriteLine("Invalid distance format!");
        }
    }

    private static void AddSwimActivity(User user)
    {
        Console.Write("Name of the swim activity: ");
        string? activityNameInput = Console.ReadLine();

        Console.Write("Enter distance in meters: ");
        string? distanceInput = Console.ReadLine();

        if (double.TryParse(distanceInput, out double distance) && distance >= 0)
        {
            // Create a DbContext instance
            using (var context = new FitnessAppContext())
            {
                var swimActivity = new SwimActivity
                {
                    Name = activityNameInput,
                    Distance = distance,
                };

                user.SwimActivities.Add(swimActivity);
                context.Users.Update(user);

                context.SaveChanges();
                Console.WriteLine("Added User and Swim Activity successfully!");
            }
        }
        else
        {
            Console.WriteLine("Invalid distance format!");
        }
    }

    private static void AddRunActivity(User user)
    {
        Console.Write("Name of the run activity: ");
        string? activityNameInput = Console.ReadLine();

        Console.Write("Enter distance in km: ");
        string? distanceInput = Console.ReadLine();

        if (double.TryParse(distanceInput, out double distance) && distance >= 0)
        {
            // Create a DbContext instance
            using (var context = new FitnessAppContext())
            {
                var runActivity = new RunActivity
                {
                    Name = activityNameInput,
                    Distance = distance,
                };

                user.RunActivities.Add(runActivity);
                context.Users.Update(user);

                context.SaveChanges();
                Console.WriteLine("Added User and Run Activity successfully!");
            }
        }
        else
        {
            Console.WriteLine("Invalid distance format!");
        }
    }

    internal static void AddUserWithoutActivities()
    {
        using (var context = new FitnessAppContext())
        {
            Console.WriteLine("Enter First Name: ");
            string? firstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name: ");
            string? lastName = Console.ReadLine();

            SportActivity sportActivity = new();

            var newUser = sportActivity.AddUser(firstName, lastName);

            context.Users.Add(newUser);
            context.SaveChanges();

            Console.WriteLine("User added successfully!");
        }
    }

    internal static void AddActivityWithoutUser()
    {
        throw new NotImplementedException();
    }

    internal static void AddActivityToUser()
    {
        throw new NotImplementedException();
    }

    internal static void PrintUsersAndActivities(int page)
    {
        using (var context = new FitnessAppContext())
        {
            int usersPerPage = 1;
            int skipCount = (page - 1) * usersPerPage;

            var user = context.Users
                .Include(u => u.BikeActivities)
                .Include(u => u.ClimbActivities)
                .Include(u => u.RunActivities)
                .Include(u => u.SwimActivities)
                .OrderBy(u => u.Id)
                .Skip(skipCount)
                .Take(usersPerPage)
                .FirstOrDefault();

            if (user != null)
            {
                Console.WriteLine($"User: {user.FirstName} {user.LastName}");

                Console.WriteLine("Bike Activities:");
                foreach (var activity in user.BikeActivities)
                {
                    Console.WriteLine($"  - Name: {activity.Name}, Distance: {activity.Distance} km");
                }

                Console.WriteLine("Climb Activities:");
                foreach (var activity in user.ClimbActivities)
                {
                    Console.WriteLine($"  - Name: {activity.Name}, Distance: {activity.Distance} meters");
                }

                Console.WriteLine("Run Activities:");
                foreach (var activity in user.RunActivities)
                {
                    Console.WriteLine($"  - Name: {activity.Name}, Distance: {activity.Distance} km");
                }

                Console.WriteLine("Swim Activities:");
                foreach (var activity in user.SwimActivities)
                {
                    Console.WriteLine($"  - Name: {activity.Name}, Distance: {activity.Distance} meters");
                }

                Console.WriteLine();
            }
        }
    }


    internal static void PrintSpecificUserAndActivities()
    {
        Console.WriteLine("Enter User's Id to view their details and activities: ");
        string? userIdInput = Console.ReadLine();

        if (int.TryParse(userIdInput, out int userId))
        {
            SportActivity sportActivity = new();
            sportActivity.GetUserAndActivities(userId);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer ID.");
        }
    }

    public static void RetrieveAndUpdateUserDetailsByUserId()
    {
        Console.WriteLine("Enter User's Id to find: ");
        string? idToFindString = Console.ReadLine();

        if (int.TryParse(idToFindString, out int idToFind))
        {
            SportActivity sportActivity = new();

            sportActivity.PrintUserId(idToFind);

            Console.WriteLine("Which user detail do you want to edit: ");
            Console.WriteLine("1. First Name \n2. Second Name");
            string? userDetailToEdit = Console.ReadLine();

            Console.WriteLine("Enter new name: ");
            string? newNameToUpdate = Console.ReadLine();

            switch (userDetailToEdit)
            {
                case "1":
                    sportActivity.UpdateFirstName(idToFind, newNameToUpdate);
                    break;
                case "2":
                    sportActivity.UpdateLastName(idToFind, newNameToUpdate);
                    break;
                default:
                    Console.WriteLine("Invalid input! Choose 1 or 2");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer ID.");
        }
    }

    internal static void RetrieveAndUpdateActivityDetailsByUserId()
    {
        Console.WriteLine("Enter User's Id to list their activities: ");
        string? idToFindString = Console.ReadLine();

        if (int.TryParse(idToFindString, out int idToFind))
        {
            SportActivity sportActivity = new();

            sportActivity.GetUserAndActivities(idToFind);

            Console.WriteLine("\nWhich Activity ID do you want to edit: ");
            Console.WriteLine("Enter a number: ");
            string? activityIdString = Console.ReadLine();

            if (int.TryParse(activityIdString, out int activityId))
            {
                if (!sportActivity.PrintOneActivityByItsId(idToFind, activityId))
                {
                    return;
                }

                Console.WriteLine("Which Activity detail do you want to edit: ");
                Console.WriteLine("1. Activity Name \n2. Activity Distance");
                string? newActivityDetails = Console.ReadLine();

                switch (newActivityDetails)
                {
                    case "1":
                        Console.WriteLine("Enter new activity name: ");
                        string? newActivityName = Console.ReadLine();
                        sportActivity.UpdateActivityName(idToFind, activityId, newActivityName);
                        break;
                    case "2":
                        Console.WriteLine("Enter new activity distance: ");
                        string? newDistance = Console.ReadLine();
                        sportActivity.UpdateActivityDistance(idToFind, activityId, newDistance);
                        break;
                    default:
                        Console.WriteLine("Invalid input! Choose 1 or 2");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer ID.");
        }
    }

    internal static void DeleteUserAndActivities()
    {
        throw new NotImplementedException();
    }

    internal static void DeleteActivityForUserId()
    {
        throw new NotImplementedException();
    }
}