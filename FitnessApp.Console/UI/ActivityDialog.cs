public class ActivityDialog
{
    internal static void EnterNewUserAndActivity()
    {
        throw new NotImplementedException();
    }

    internal static void LoadUserActivities()
    {
        throw new NotImplementedException();
    }

    public static void RetrieveAndUpdateUser()
    {
        Console.WriteLine("Enter User's Firstname to find: ");
        string? firstNameToFind = Console.ReadLine();

        Console.WriteLine("Enter the new First Name: ");
        string? newFirstName = Console.ReadLine();

        SportActivity sportActivity = new SportActivity();

        sportActivity.FindUserByFirstName(firstNameToFind);
        sportActivity.UpdateUserFirstName(firstNameToFind, newFirstName);
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