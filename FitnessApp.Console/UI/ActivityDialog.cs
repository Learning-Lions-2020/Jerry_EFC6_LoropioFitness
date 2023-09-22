using FitnessApp.Data;
using FitnessApp.Domain.Users;

public class ActivityDialog
{
    internal static void EnterNewUserAndActivity()
    {
        SportActivity sportActivity = new();

        Console.WriteLine("Enter First Name: ");
        string? firstName = Console.ReadLine();

        Console.WriteLine("Enter Last Name: ");
        string? lastName = Console.ReadLine();

        User userName = sportActivity.AddUser(firstName, lastName);

        Console.WriteLine("What type of activity do you want to enter?");
        Console.WriteLine("1. Run Activity\n2. Swim Activity\n3. Bike Activity\n4. Climb Activity");
        string? activitySelection = Console.ReadLine();

        Console.WriteLine("Enter Activity Name(format example: Run/Swim/Bike/Climb From Loropio to Lokitaungber");
        string? activityName = Console.ReadLine();

        Console.WriteLine("Enter Activity Distance: ");
        string? activityDistanceString = Console.ReadLine();

        if (double.TryParse(activityDistanceString, out double activityDistance))
        {
            switch (activitySelection)
            {
                case "1":
                    sportActivity.AddRunActivity(userName, activityName, activityDistance);
                    break;
                case "2":
                    sportActivity.AddSwimActivity(userName, activityName, activityDistance);
                    break;
                case "3":
                    sportActivity.AddBikeActivity(userName, activityName, activityDistance);
                    break;
                case "4":
                    sportActivity.AddClimbActivity(userName, activityName, activityDistance);
                    break;
                default:
                    throw new ArgumentException("Invalid selection");
            }
        }
        else
        {
            Console.WriteLine("Invalid input for Activity Distance. Please enter a valid number.");
        }
        sportActivity.SaveChanges();
    }

    internal static void LoadUserAndActivities()
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

    internal static void LoadAllUsersAndActivities()
    {
        throw new NotImplementedException();
    }

    public static void RetrieveAndUpdateUser()
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

    internal static void RetrieveAndUpdateActivity()
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

    internal static void DeleteUser()
    {
        throw new NotImplementedException();
    }

    internal static void DeleteActivityForUserId()
    {
        throw new NotImplementedException();
    }
}