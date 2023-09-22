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

            sportActivity.FindUserById(idToFind);

            sportActivity.PrintUserWithId(idToFind);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer ID.");
        }
    }

    internal static void RetrieveAndUpdateActivity()
    {
        throw new NotImplementedException();
    }

    internal static void DeleteUser()
    {
        throw new NotImplementedException();
    }

    internal static void DeleteActivityForUser()
    {
        throw new NotImplementedException();
    }
}