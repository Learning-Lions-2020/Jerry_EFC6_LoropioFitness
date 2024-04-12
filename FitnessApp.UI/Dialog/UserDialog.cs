using FitnessApp.Data.Repository;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.CustomTypes;
using FitnessApp.Domain.Entities.Base;
using FitnessApp.Domain.Entitities;
using FitnessApp.Domain.Security;

namespace FitnessApp.UI.Dialog;

public class UserDialog
{
    private IUserRepository userRepository;
    private User user;
    private User _currentUser;
    private Guid activityId;

    public UserDialog()
    {
        userRepository = new UserRepository();
        // in the constructor of the UserDialog we create a UserRepository and a User
        // the repository is passed to the User in his constructor so that a User Domain Object has access to his repository
        user = new User(userRepository);
    }

    public void StartLogonDialog()
    {
        // Display options to create a new user account, log in, or quit
        Console.WriteLine("Welcome to Loropio Fitness App!");
        Console.WriteLine("1. Create a new user account");
        Console.WriteLine("2. Log in");
        Console.WriteLine("3. Change username or password");
        Console.WriteLine("4. Quit");

        // Get user input
        string input = Console.ReadLine();

        // Process user input
        switch (input)
        {
            case "1":
                ShowRegisterNewUserDialog();
                break;
            case "2":
                ShowLogonDialog();
                break;
            case "3":
                ChangeUsernameOrPassword();
                break;
            case "4":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                StartLogonDialog();
                break;
        }
    }

    private void ShowRegisterNewUserDialog()
    {
        // Task 2:Create a Dialog for User to Register with his UserName and his Password

        Console.WriteLine("Enter your username:");
        string userNameInput = Console.ReadLine();

        Console.WriteLine("Enter your password:");
        string passwordInput = Console.ReadLine();

        // Task 3: Uncomment the lines below and make the work. Use the already implemented Register method for the User

        if (!string.IsNullOrEmpty(userNameInput) && !string.IsNullOrEmpty(passwordInput))
        {
            user.Register(userNameInput, passwordInput);
            Console.WriteLine("User registered successfully!");
        }
        else
        {
            Console.WriteLine("You did not enter valid credentials !");
        }
    }

    private void ShowLogonDialog()
    {
        // Task 4: Create the Dialog to Logon with Username and Password

        Console.WriteLine("Enter your username:");
        string userNameInput = Console.ReadLine();

        Console.WriteLine("Enter your password:");
        string passwordInput = Console.ReadLine();

        // uncomment the lines below and make the work

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
        // Task 5 Add the Dialog to Enter a new Sport Activity
        // Use the existing ActivityDialog class to enter the Sport Activity

        ActivityDialog activitydialog = new ActivityDialog();
        activitydialog.SetUserId(user.Id);
        activitydialog.ActivityMenu();

    }


    

    public void ChangeUsernameOrPassword()
    {
        Console.WriteLine("Select what you want to change:");
        Console.WriteLine("1. Change username");
        Console.WriteLine("2. Change password");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                ChangeUsername();
                break;
            case "2":
                ChangePassword();
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                ChangeUsernameOrPassword();
                break;
        }
    }

    public void ChangeUsername()
    {
        Console.WriteLine("Enter your new username:");
        string newUsername = Console.ReadLine();

        if (!string.IsNullOrEmpty(newUsername))
        {
            user.UserName = newUsername;
            user.SaveOrUpdate();
            Console.WriteLine("Username changed successfully!");
        }
        else
        {
            Console.WriteLine("Invalid username. Please try again.");
            ChangeUsername();
        }
    }

    public void ChangePassword()
    {
        Console.WriteLine("Enter your current password:");
        string currentPassword = Console.ReadLine();

        if (SecurityProvider.VerifyPassword(currentPassword, user.PasswordHash, user.PasswordSalt))
        {
            Console.WriteLine("Enter your new password:");
            string newPassword = Console.ReadLine();

            if (!string.IsNullOrEmpty(newPassword))
            {
                // Hash the new password and update user's password hash and salt
                string newHash = SecurityProvider.HashPasword(newPassword, out var newSalt);
                user.PasswordHash = newHash;
                user.PasswordSalt = Convert.ToHexString(newSalt);
                user.SaveOrUpdate();
                Console.WriteLine("Password changed successfully!");
            }
            else
            {
                Console.WriteLine("Invalid password. Please try again.");
                ChangePassword();
            }
        }
        else
        {
            Console.WriteLine("Incorrect current password. Please try again.");
            ChangePassword();
        }
    }

}