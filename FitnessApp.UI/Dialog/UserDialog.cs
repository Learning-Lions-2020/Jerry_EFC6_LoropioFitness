using FitnessApp.Domain.Entitities;
using FitnessApp.Domain.Security;

namespace FitnessApp.UI.Dialog;

public class UserDialog : BaseDialog
{

    public UserDialog(User user) : base(user) { }

    public void ManageProfile()
    {
        do
        {
            // User profile management logic
            Console.WriteLine("User profile management");
            Console.WriteLine("1. Change username");
            Console.WriteLine("2. Change password");
            Console.WriteLine("3. Back");
            Console.WriteLine("4. Log out");

            var authenticationDialog = new AuthenticationDialog(ServiceProvider);

            // Get user input
            string input = Console.ReadLine();

            // Process user input
            switch (input)
            {

                case "1":
                    ChangeUsername();
                    break;
                case "2":
                    ChangePassword();
                    break;
                case "3":
                    authenticationDialog.Menu();
                    break;
                case "4":
                    authenticationDialog.Logout();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    ManageProfile();
                    break;
            }
        } while (true);
    }


    public void ChangeUsername()
    {
        Console.WriteLine($"Your current username is {LoggedUser.UserName}");

        Console.WriteLine("Enter your new username:");
        string? newUsername = Console.ReadLine();

        LoggedUser.UserName = newUsername;
        LoggedUser.SaveOrUpdate();
        Console.WriteLine("Username changed successfully");

        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }

    public void ChangePassword()
    {
        Console.WriteLine("Enter your current password:");
        string currentPassword = Console.ReadLine();

        if (SecurityProvider.VerifyPassword(currentPassword, LoggedUser.PasswordHash, LoggedUser.PasswordSalt))
        {
            Console.WriteLine("Enter your new password:");
            string newPassword = Console.ReadLine();

            if (!string.IsNullOrEmpty(newPassword))
            {
                // Hash the new password and update user's password hash and salt
                string newHash = SecurityProvider.HashPasword(newPassword, out var newSalt);
                LoggedUser.PasswordHash = newHash;
                LoggedUser.PasswordSalt = Convert.ToHexString(newSalt);
                LoggedUser.SaveOrUpdate();
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