public static class Dialog
{
    public static void StartDialog()
    {
        string? userSelection;

        do
        {
            //print the first screen
            //this is another comment
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************************");
            Console.WriteLine("* Welcome to the Loropio Fitness App *");
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("********************");
            Console.WriteLine("* Select an action *");
            Console.WriteLine("********************");

            Console.WriteLine("1: Add user with activity");
            Console.WriteLine("2: Add user without activity");
            Console.WriteLine("3: Add activity without user");
            Console.WriteLine("4. Print all users and their activities");
            Console.WriteLine("5: Print Specific User and Activities");
            Console.WriteLine("6: Retrieve and Update User by Id");
            Console.WriteLine("7: Retrieve and Update Activity by User Id");
            Console.WriteLine("8: Delete User");
            Console.WriteLine("9: Delete Activity by User Id");
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
                    ActivityDialog.AddActivityWithoutUser();
                    break;
                case "4":
                    ActivityDialog.PrintUsersAndActivities();
                    break;
                case "5":
                    ActivityDialog.PrintSpecificUserAndActivities();
                    break;
                case "6":
                    ActivityDialog.RetrieveAndUpdateUser();
                    break;
                case "7":
                    ActivityDialog.RetrieveAndUpdateActivity();
                    break;
                case "8":
                    ActivityDialog.DeleteUser();
                    break;
                case "9":
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