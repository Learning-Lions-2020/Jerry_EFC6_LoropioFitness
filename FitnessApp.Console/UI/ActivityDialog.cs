public class ActivityDialog
{
    internal static void EnterNewUserAndActivity()
    {
        throw new NotImplementedException();
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