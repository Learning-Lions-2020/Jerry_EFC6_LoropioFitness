using FitnessApp.Data.Repository;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entities;
using Microsoft.Identity.Client;

namespace FitnessApp.UI.Dialog;

public class UserDialog
{
    private IUserRepository userRepository;
    private User user;

    public UserDialog()
    {
        userRepository = new UserRepository();
        user = new User(userRepository);
    }

    public void StartLogonDialog()
    {
        string? userSelection;
        do
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************************");
            Console.WriteLine("* Welcome to the Loropio Fitness App *");
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("********************************************************");
            Console.WriteLine("* Please Logon to the App or create a new User Account *");
            Console.WriteLine("********************************************************");

            Console.WriteLine("1: Logon");
            Console.WriteLine("2: Create a new User Account");
            Console.WriteLine("99: Quit application");
            Console.Write("Your selection: ");

            userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    ShowLogonDialog();
                    break;
                case "2":
                    ShowRegisterNewUserDialog();
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

    private void ShowRegisterNewUserDialog()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("**************************************");
        Console.WriteLine("* Please enter your Credentials ");
        Console.WriteLine("**************************************");

        Console.WriteLine("Enter your User Name:");
        var userNameInput = Console.ReadLine();

        Console.WriteLine("Enter your Password:");
        var passwordInput = Console.ReadLine();
        
        

        if (!string.IsNullOrEmpty(userNameInput) && !string.IsNullOrEmpty(passwordInput))
        {
            user.Register(userNameInput, passwordInput);
        }
        else
        {
            Console.WriteLine("You did not enter valid credentials !");
        }
    }

    private void ShowLogonDialog()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("**************************************");
        Console.WriteLine("* Please enter your Credentials ");
        Console.WriteLine("**************************************");
        
        Console.WriteLine("Enter your User Name:");
        var userNameInput = Console.ReadLine();

        Console.WriteLine("Enter your Password:");
        var passwordInput = Console.ReadLine();
        
        if (!string.IsNullOrEmpty(userNameInput) && !string.IsNullOrEmpty(passwordInput))
        {
             var credentialsAreValid = user.GetCredentialsAreValid(userNameInput, passwordInput);

            if (credentialsAreValid)
            {
                Console.WriteLine($"Welcome {user.UserName}, you have logged on successfully !");
                ShowActivityDialog();
            }
            else
            {
                Console.WriteLine("You did not enter valid credentials !");
            }
        }
        else
        {
            Console.WriteLine("You did not provide your User Name username or Password !");
        }
    }

    private void ShowActivityDialog()
    {
        string? userSelection;
        do
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************************");
            Console.WriteLine("* Welcome to the Loropio Fitness App *");
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("1: Enter a new Sport Activity");
            Console.WriteLine("99: Quit application");
            Console.Write("Your selection: ");

            userSelection = Console.ReadLine();
            
            var activityDialog = new ActivityDialog();
            activityDialog.SetUserId(user.Id);

            switch (userSelection)
            {
                case "1":
                    activityDialog.EnterActivity();
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