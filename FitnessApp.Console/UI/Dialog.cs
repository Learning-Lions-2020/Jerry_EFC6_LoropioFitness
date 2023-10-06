public static class Dialog
{
    public static void StartDialog()
    {
        string? userSelection;

        do
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************************");
            Console.WriteLine("* Welcome to the Loropio Fitness App *");
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("********************");
            Console.WriteLine("* Select an action *");
            Console.WriteLine("********************");

            Console.WriteLine("1: Add User with Activity");
            Console.WriteLine("2: Add User without Activity");
            Console.WriteLine("3: Add Activity without User");
            Console.WriteLine("4: Add an Activity to a User");
            Console.WriteLine("5. Print all Users and their Activities");
            Console.WriteLine("6: Print Specific User and Activities");
            Console.WriteLine("7: Retrieve and Update User Details by User Id");
            Console.WriteLine("8: Retrieve and Update Activity Details by User Id");
            Console.WriteLine("9: Delete User and Activities");
            Console.WriteLine("10: Delete Activity by User Id");
            Console.WriteLine("99: Quit application");
            Console.Write("Your selection: ");

            userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    ActivityDialog.AddUserWithActivities();
                    break;
                case "2":
                    ActivityDialog.AddUserWithoutActivities();
                    break;
                case "3":
                    ActivityDialog.AddActivitiesWithoutUser();
                    break;
                case "4":
                    ActivityDialog.AddActivityToUser();
                    break;
                case "5":
                    ActivityDialog.PrintUsersAndActivities();
                    break;
                case "6":
                    ActivityDialog.PrintSpecificUserAndActivities();
                    break;
                case "7":
                    ActivityDialog.RetrieveAndUpdateUserDetailsByUserId();
                    break;
                case "8":
                    ActivityDialog.RetrieveAndUpdateActivityDetailsByUserId();
                    break;
                case "9":
                    ActivityDialog.DeleteUserAndActivities();
                    break;
                case "10":
                    ActivityDialog.DeleteActivityForUserId();
                    break;
                case "99": break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
        while (userSelection != "99");

        Console.WriteLine("Thanks for using Loropio Fitness App");
    }
}