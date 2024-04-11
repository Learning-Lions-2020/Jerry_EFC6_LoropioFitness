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
        Console.WriteLine("3. List activities by type");
        Console.WriteLine("4. List activities by date");
        Console.WriteLine("5. Change Username or Password");
        Console.WriteLine("6. Update Activity Details");
        Console.WriteLine("7. Quit");

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
                ListActivitiesByType();
                break;
            case "4":
                ListActivitiesByDate();
                break;
            case "5":
                ChangeUsernameOrPassword();
                break;
            case "6":
                UpdateActivityDetails();
                break;
            case "7":
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
        activitydialog.EnterActivity();

    }


    public void ListActivitiesByType()
    {
        Console.WriteLine("Select an activity type:");
        Console.WriteLine("1. Bike SportActivity");
        Console.WriteLine("2. Climb SportActivity");
        Console.WriteLine("3. Run SportActivity");
        Console.WriteLine("4. Swim SportActivity");

        string input = Console.ReadLine();

        if (int.TryParse(input, out int selectedType) && selectedType >= 1 && selectedType <= 4)
        {
            var selectedActivities = user.SportActivities.Where(a => (int)a.ActivityType == selectedType).ToList();
            if (selectedActivities.Any())
            {
                Console.WriteLine($"DEBUG: Found {selectedActivities.Count} activities for selected type {selectedType}");

                foreach (var activity in selectedActivities)
                {
                    Console.WriteLine($"Activity Type: {activity.ActivityType}");
                    Console.WriteLine($"Date: {activity.ActivityDate}");
                    Console.WriteLine($"Distance Covered: {activity.Distance} {activity.DistanceUnit}");
                    Console.WriteLine($"Time Taken: {activity.TimeTaken}");
                    Console.WriteLine($"Feeling: {activity.Feeling}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No activities found for the selected type.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please select a valid activity type.");
        }
    }

    public void ListActivitiesByDate()
    {
        Console.WriteLine("Enter the date in YYYY/MM/DD format:");
        string dateInput = Console.ReadLine();

        if (DateTime.TryParseExact(dateInput, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime selectedDate))
        {
            var selectedActivities = user.SportActivities.Where(a => a.ActivityDate.Date == selectedDate.Date).ToList();
            if (selectedActivities.Any())
            {
                Console.WriteLine($"Activities for selected date:");

                foreach (var activity in selectedActivities)
                {
                    Console.WriteLine($"Activity Type: {activity.ActivityType}");
                    Console.WriteLine($"Distance Covered: {activity.Distance} {activity.DistanceUnit}");
                    Console.WriteLine($"Time Taken: {activity.TimeTaken}");
                    Console.WriteLine($"Feeling: {activity.Feeling}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No activities found for the selected date.");
            }
        }
        else
        {
            Console.WriteLine("Invalid date format. Please enter the date in YYYY/MM/DD format.");
        }
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

    public void UpdateActivityDetails()
    {
        Console.WriteLine("Enter the ID of the activity to update:");
        string activityIdInput = Console.ReadLine();

        if (int.TryParse(activityIdInput, out int activityId))
        {
            var activityToUpdate = user.SportActivities.FirstOrDefault(a => a.Id == activityId);

            if (activityToUpdate != null)
            {
                UpdateActivityFields(activityToUpdate);
                user.SaveOrUpdate();
                Console.WriteLine("Activity details updated successfully!");
            }
            else
            {
                Console.WriteLine("Activity not found. Please enter a valid ID.");
                UpdateActivityDetails();
            }
        }
        else
        {
            Console.WriteLine("Invalid activity ID. Please enter a valid integer.");
            UpdateActivityDetails();
        }
    }

    private void UpdateActivityFields(SportActivity activity)
    {
        Console.WriteLine("Enter the new distance covered on the activity in KM:");
        string distanceInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(distanceInput) && double.TryParse(distanceInput, out double newDistance))
        {
            activity.Distance = newDistance;
        }
        else
        {
            Console.WriteLine("Invalid distance input. Distance must be a valid number in kilometers.");
        }

        Console.WriteLine("Enter the new time taken on the activity in the format HH:MM:SS:");
        string timeInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(timeInput) && TimeSpan.TryParseExact(timeInput, "hh\\:mm\\:ss", null, out TimeSpan newTime))
        {
            activity.TimeTaken = newTime;
        }
        else
        {
            Console.WriteLine("Invalid time input. Time must be in the format HH:MM:SS.");
        }

        Console.WriteLine("Enter the new date of the activity in the format YYYY/MM/DD:");
        string dateInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(dateInput) && DateTime.TryParseExact(dateInput, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime newDate))
        {
            activity.ActivityDate = newDate;
        }
        else
        {
            Console.WriteLine("Invalid date input. Date must be in the format YYYY/MM/DD.");
        }

        Console.WriteLine("Enter the new feeling after the activity:");
        Console.WriteLine("1. BAD");
        Console.WriteLine("2. OK");
        Console.WriteLine("3. GOOD");
        Console.WriteLine("4. STRONG");
        Console.WriteLine("5. VERY STRONG");
        string feelingInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(feelingInput) && Enum.TryParse(feelingInput, out Feeling newFeeling) && Enum.IsDefined(typeof(Feeling), newFeeling))
        {
            activity.Feeling = newFeeling;
        }
        else
        {
            Console.WriteLine("Invalid feeling input. Please select a valid feeling option.");
        }
    }

}