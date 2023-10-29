using FitnessApp.Data.Repository;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entitities;
using Microsoft.Extensions.Logging;

namespace FitnessApp.UIServices.Dialog;

public class UserDialog
{
    private IUserRepository _userRepository = new UserRepository();
    private User _user;
    public void StartDialog()
    {
        string? userSelection;
        _user = new User(_userRepository);
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
            _user.Register(userNameInput, passwordInput);
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
            var credentialsAreValid = _user.GetCredentialsAreValid(userNameInput, passwordInput);

            if (credentialsAreValid)
                Console.WriteLine("Welcome !");
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
}