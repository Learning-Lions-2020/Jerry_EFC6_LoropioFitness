using FitnessApp.Console.Activity;
using FitnessApp.Data;
using FitnessApp.Domain.Activities;
using FitnessApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

public class ActivityDialog
{
    private static ActivityType[] validActivities = { ActivityType.BikeActivity, ActivityType.ClimbActivity, ActivityType.RunActivity, ActivityType.SwimActivity };
    private static int currentPage = 1;
    private static int pageSize = 1;

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

    internal static void AddActivitiesWithoutUser()
    {
        Console.WriteLine("What type of activity do you want to enter?");
        Console.WriteLine("1. Bike Activity\n2. Climb Activity\n3. Run Activity\n4. Swim Activity");
        Console.Write("Your selection: ");

        if (int.TryParse(Console.ReadLine(), out int selectedActivityIndex) && selectedActivityIndex >= 1 && selectedActivityIndex <= validActivities.Length)
        {
            ActivityType activityType = validActivities[selectedActivityIndex - 1];
            OpenActivityDialog(null, activityType); // User parameter is null for activities without a user
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    internal static void AddActivityToUser()
    {
        Console.WriteLine("Which user do you want to add an activity to? Enter user id:");
        if (int.TryParse(Console.ReadLine(), out int userId))
        {
            using (var context = new FitnessAppContext())
            {
                var user = context.Users
                    .Include(u => u.BikeActivities)
                    .Include(u => u.ClimbActivities)
                    .Include(u => u.RunActivities)
                    .Include(u => u.SwimActivities)
                    .FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    Console.WriteLine($"User with id {userId} not found.");
                    return;
                }

                Console.WriteLine("Do you want to add an existing activity or a new activity?");
                Console.WriteLine("1. Existing Activity\n2. New Activity");
                Console.Write("Your selection: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AssignExistingActivityToUser(context, user);
                            break;
                        case 2:
                            AddNewActivityToUser(context, user);
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid user id format.");
        }
    }

    private static void AssignExistingActivityToUser(FitnessAppContext context, User user)
    {
        Console.WriteLine("Existing Activities available for assignment:");

        var availableBikeActivities = context.BikeActivities
            .Where(activity => activity.UserId == null)
            .ToList();

        var availableClimbActivities = context.ClimbActivities
            .Where(activity => activity.UserId == null)
            .ToList();

        var availableRunActivities = context.RunActivities
            .Where(activity => activity.UserId == null)
            .ToList();

        var availableSwimActivities = context.SwimActivities
            .Where(activity => activity.UserId == null)
            .ToList();

        //specific activity to print available activities
        foreach (var activity in availableBikeActivities)
        {
            Console.WriteLine($"Activity Id: {activity.Id}, Name: {activity.Name}, Distance: {activity.Distance} km");
        }

        foreach (var activity in availableClimbActivities)
        {
            Console.WriteLine($"Activity Id: {activity.Id}, Name: {activity.Name}, Distance: {activity.Distance} meters");
        }

        foreach (var activity in availableRunActivities)
        {
            Console.WriteLine($"Activity Id: {activity.Id}, Name: {activity.Name}, Distance: {activity.Distance} km");
        }

        foreach (var activity in availableSwimActivities)
        {
            Console.WriteLine($"Activity Id: {activity.Id}, Name: {activity.Name}, Distance: {activity.Distance} meters");
        }

        Console.Write("Enter the Activity Id to assign to the user: ");
        if (int.TryParse(Console.ReadLine(), out int activityId))
        {
            // Check the specific activity type variables
            var bikeActivity = availableBikeActivities.FirstOrDefault(a => a.Id == activityId);
            var climbActivity = availableClimbActivities.FirstOrDefault(a => a.Id == activityId);
            var runActivity = availableRunActivities.FirstOrDefault(a => a.Id == activityId);
            var swimActivity = availableSwimActivities.FirstOrDefault(a => a.Id == activityId);

            if (bikeActivity != null)
            {
                bikeActivity.UserId = user.Id;
                context.SaveChanges();
                Console.WriteLine($"Successfully added activity to user: {user.FirstName} {user.LastName}");
            }
            else if (climbActivity != null)
            {
                climbActivity.UserId = user.Id;
                context.SaveChanges();
                Console.WriteLine($"Successfully added activity to user: {user.FirstName} {user.LastName}");
            }
            else if (runActivity != null)
            {
                runActivity.UserId = user.Id;
                context.SaveChanges();
                Console.WriteLine($"Successfully added activity to user: {user.FirstName} {user.LastName}");
            }
            else if (swimActivity != null)
            {
                swimActivity.UserId = user.Id;
                context.SaveChanges();
                Console.WriteLine($"Successfully added activity to user: {user.FirstName} {user.LastName}");
            }
            else
            {
                Console.WriteLine("Invalid activity Id.");
            }
        }
        else
        {
            Console.WriteLine("Invalid Activity Id format.");
        }
    }


    private static void AddNewActivityToUser(FitnessAppContext context, User user)
    {

    }


    internal static void PrintUsersAndActivities()
    {
        while (true)
        {
            int nextPage = PrintUsersAndActivities(currentPage, pageSize);

            if (nextPage == -1)
            {
                break;
            }

            currentPage = nextPage;
        }
    }

    internal static int PrintUsersAndActivities(int currentPage, int pageSize)
    {
        using (var context = new FitnessAppContext())
        {
            var users = context.Users
                .Include(u => u.BikeActivities)
                .Include(u => u.ClimbActivities)
                .Include(u => u.RunActivities)
                .Include(u => u.SwimActivities)
                .OrderBy(u => u.Id)
                .ToList();

            int totalUsers = users.Count;
            int totalPages = (int)Math.Ceiling((double)totalUsers / pageSize); //Math.Ceiling is not covered in the course

            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, totalUsers);

            for (int i = startIndex; i < endIndex; i++)
            {
                var user = users[i];
                Console.WriteLine($"\nUser: {user.FirstName} {user.LastName}");
                PrintUserActivities(user);
                Console.WriteLine();
            }

            Console.WriteLine($"Page {currentPage}/{totalPages}");
            Console.WriteLine("Press 'N' for the next page or any other key to exit...\n");
            var key = Console.ReadKey().Key;

            if (key == ConsoleKey.N && currentPage < totalPages)
            {
                return currentPage + 1;
            }
            else
            {
                return -1;
            }
        }
    }

    private static void PrintUserActivities(User user)
    {
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
        Console.WriteLine("Enter User's Id to view their details and activities before deletion: ");
        string? userIdInput = Console.ReadLine();

        SportActivity sportActivity = new();

        if (int.TryParse(userIdInput, out int userId))
        {
            sportActivity.GetUserAndActivities(userId);

            using (var context = new FitnessAppContext())
            {
                var user = context.Users.Include(u => u.BikeActivities)
                                      .Include(u => u.ClimbActivities)
                                      .Include(u => u.RunActivities)
                                      .Include(u => u.SwimActivities)
                                      .FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    Console.WriteLine("Press Enter to confirm deletion, or any other key to cancel...");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        context.Users.Remove(user);
                        context.SaveChanges();

                        Console.WriteLine("User and associated activities deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Deletion canceled.");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer ID.");
        }
    }


    internal static void DeleteActivityForUserId()
    {
        throw new NotImplementedException();
    }
}