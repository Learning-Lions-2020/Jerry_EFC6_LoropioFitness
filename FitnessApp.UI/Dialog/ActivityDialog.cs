using FitnessApp.Data.DBContext;
using FitnessApp.Domain.CustomTypes;
using FitnessApp.Domain.Entities.Base;
using FitnessApp.Domain.Entitities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FitnessApp.UI.Dialog;

public class ActivityDialog : BaseDialog
{
    private readonly FitnessAppContext _dbContext;

    public ActivityDialog(User user) : base(user) { }



    public void ManageSportActivities()
    {
        do
        {

            // Sport activities management logic
            Console.WriteLine("Sport activities management logic...");
            Console.WriteLine("1. Add activity");
            Console.WriteLine("2. View all activities");
            Console.WriteLine("3. List activities by type");
            Console.WriteLine("4. List activities by date");
            Console.WriteLine("5. Update activity details");
            Console.WriteLine("6. Delete activity by id");
            Console.WriteLine("7. Remove all activities");
            Console.WriteLine("8. Back");
            Console.WriteLine("9. Log out");

            var authenticationDialog = new AuthenticationDialog(ServiceProvider);


            // Get user input
            string input = Console.ReadLine();

            // Process user input
            switch (input)
            {
                case "1":
                    AddActivity();
                    break;
                case "2":
                    ViewAllActivities();
                    break;
                case "3":
                    ListActivitiesByType();
                    break;
                case "4":
                    ListActivitiesByDate();
                    break;
                case "5":
                    UpdateActivityDetails();
                    break;
                case "6":
                    DeleteActivityById();
                    break;
                case "7":
                    RemoveAllActivities();
                    break;
                case "8":
                    authenticationDialog.Menu();
                    break;
                case "9":
                    authenticationDialog.Logout();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    ManageSportActivities();
                    break;
            }

        } while (true);

    }

    void AddActivity()
    {
        // Task 6: Give the possibility to select and enter a activity

        string[] validActivityTypeInput = new string[4] { "1", "2", "3", "4" };

        Console.WriteLine("What type of sports activity do you want to enter ?");
        Console.WriteLine("1. Bike SportActivity");
        Console.WriteLine("2. Climb SportActivity");
        Console.WriteLine("3. Run SportActivity");
        Console.WriteLine("4. Swim SportActivity");

        string activityTypeInput = Console.ReadLine();

        int selection;

        bool isValidEntry = int.TryParse(activityTypeInput, out selection) && validActivityTypeInput.Contains(activityTypeInput);

        if (!isValidEntry)
        {
            Console.WriteLine("Enter a valid activity type");
            Console.ReadLine();
            return;
        }

        var activityType = (ActivityType)int.Parse(activityTypeInput); 

        OpenActivityDialog(activityType);
    }

    void OpenActivityDialog(ActivityType activityType)
    {
        switch (activityType)
        {
            case ActivityType.RunActivity:
                AddRunActivity();
                break;
            case ActivityType.BikeActivity:
                AddBikeActivity();
                break;
            case ActivityType.ClimbActivity:
                AddClimbActivity();
                break;
            case ActivityType.SwimActivity:
                AddSwimActivity();
                break;
        }
        // Task 7 : add the code to add a Bike Activity

        void AddRunActivity()
        {
            try
            {
                Console.WriteLine("Enter the total distance covered on the activity in KM");
                string? validActivityTypeInput = Console.ReadLine();

                if (string.IsNullOrEmpty(validActivityTypeInput)) throw new ArgumentException("Please enter a valid distance");
                double distanceCovered = double.Parse(validActivityTypeInput);

                Console.WriteLine("Enter the total time spent on the activity in the format HH:MM:SS");
                string? timeTakenInput = Console.ReadLine();
                if (string.IsNullOrEmpty(timeTakenInput)) throw new ArgumentException("Please enter the valid time in the defined format");
                TimeSpan timeTaken = TimeSpan.Parse(timeTakenInput);

                Console.WriteLine("Enter the date of the activity in the format YYYY/MM/DD");
                string? dateOfActivityInput = Console.ReadLine();
                if (string.IsNullOrEmpty(dateOfActivityInput)) throw new ArgumentException("Please enter a valid date");
                DateTime dateOfActivity = DateTime.Parse(dateOfActivityInput);


                Console.WriteLine("How did you feel after the activity: ");
                Console.WriteLine("1. BAD");
                Console.WriteLine("2. OK");
                Console.WriteLine("3. GOOD");
                Console.WriteLine("4. STROMG");
                Console.WriteLine("5. VERY STRONG");
                string afterActivityFeeling = Console.ReadLine();


                if (afterActivityFeeling != null)
                {
                    Feeling feeling = (Feeling)Enum.Parse(typeof(Feeling), afterActivityFeeling);
                    // Task 8 : add the code to create a run activity add the activity to the users activities and save the user with the activity

                    // Create a new RunActivity object
                    var runActivity = new RunActivity
                    {
                        Distance = distanceCovered,
                        TimeTaken = timeTaken,
                        ActivityDate = dateOfActivity,
                        Feeling = feeling
                    };

                    LoggedUser?.AddActivity(runActivity);
                    LoggedUser?.SaveOrUpdate();
                }
                Console.WriteLine("New run Activity created and saved.");
            }

            // Task 9 : add further exceptions to account for conversion and format problems

            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }

        }


        void AddBikeActivity()
        {
            try
            {
                Console.WriteLine("Enter the total distance covered on the bike activity in KM:");
                string distanceInput = Console.ReadLine();
                if (string.IsNullOrEmpty(distanceInput))
                    throw new ArgumentException("Please enter a valid distance.");

                if (!double.TryParse(distanceInput, out double distanceCovered) || distanceCovered <= 0)
                    throw new ArgumentException("Distance must be a valid positive number.");

                Console.WriteLine("Enter the total time spent on the bike activity in the format HH:MM:SS:");
                string timeTakenInput = Console.ReadLine();
                if (string.IsNullOrEmpty(timeTakenInput))
                    throw new ArgumentException("Please enter the valid time in the defined format.");

                if (!TimeSpan.TryParseExact(timeTakenInput, "hh\\:mm\\:ss", null, out TimeSpan timeTaken))
                    throw new FormatException("Time format is invalid.");

                Console.WriteLine("Enter the date of the bike activity in the format YYYY/MM/DD:");
                string dateOfActivityInput = Console.ReadLine();
                if (string.IsNullOrEmpty(dateOfActivityInput))
                    throw new ArgumentException("Please enter a valid date.");

                if (!DateTime.TryParseExact(dateOfActivityInput, "yyyy/MM/dd", null,
                    System.Globalization.DateTimeStyles.None, out DateTime dateOfActivity))
                    throw new FormatException("Date format is invalid.");

                Console.WriteLine("How did you feel after the bike activity?");
                Console.WriteLine("1. BAD");
                Console.WriteLine("2. OK");
                Console.WriteLine("3. GOOD");
                Console.WriteLine("4. STRONG");
                Console.WriteLine("5. VERY STRONG");
                string feelingInput = Console.ReadLine();

                if (!Enum.TryParse(feelingInput, out Feeling feeling) || !Enum.IsDefined(typeof(Feeling), feeling))
                    throw new ArgumentException("Invalid feeling selected.");

                // Create a new BikeActivity object
                BikeActivity bikeActivity = new BikeActivity
                {
                    Distance = distanceCovered,
                    TimeTaken = timeTaken,
                    ActivityDate = dateOfActivity,
                    Feeling = feeling
                };

                LoggedUser?.AddActivity(bikeActivity);
                LoggedUser?.SaveOrUpdate();

                Console.WriteLine("New bike activity created and saved.");

            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }


        void AddClimbActivity()
        {
            try
            {
                Console.WriteLine("Enter the total distance covered on the climb activity in M:");
                string distanceInput = Console.ReadLine();
                if (string.IsNullOrEmpty(distanceInput))
                    throw new ArgumentException("Please enter a valid distance.");

                if (!double.TryParse(distanceInput, out double distanceCovered) || distanceCovered <= 0)
                    throw new ArgumentException("Distance must be a valid positive number.");

                Console.WriteLine("Enter the total time spent on the climb activity in the format HH:MM:SS:");
                string timeTakenInput = Console.ReadLine();
                if (string.IsNullOrEmpty(timeTakenInput))
                    throw new ArgumentException("Please enter the valid time in the defined format.");

                if (!TimeSpan.TryParseExact(timeTakenInput, "hh\\:mm\\:ss", null, out TimeSpan timeTaken))
                    throw new FormatException("Time format is invalid.");

                Console.WriteLine("Enter the date of the climb activity in the format YYYY/MM/DD:");
                string dateOfActivityInput = Console.ReadLine();
                if (string.IsNullOrEmpty(dateOfActivityInput))
                    throw new ArgumentException("Please enter a valid date.");

                if (!DateTime.TryParseExact(dateOfActivityInput, "yyyy/MM/dd", null,
                    System.Globalization.DateTimeStyles.None, out DateTime dateOfActivity))
                    throw new FormatException("Date format is invalid.");

                Console.WriteLine("How did you feel after the climb activity?");
                Console.WriteLine("1. BAD");
                Console.WriteLine("2. OK");
                Console.WriteLine("3. GOOD");
                Console.WriteLine("4. STRONG");
                Console.WriteLine("5. VERY STRONG");
                string feelingInput = Console.ReadLine();

                if (!Enum.TryParse(feelingInput, out Feeling feeling) || !Enum.IsDefined(typeof(Feeling), feeling))
                    throw new ArgumentException("Invalid feeling selected.");

                // Create a new ClimbActivity object
                ClimbActivity climbActivity = new ClimbActivity
                {
                    Distance = distanceCovered,
                    TimeTaken = timeTaken,
                    ActivityDate = dateOfActivity,
                    Feeling = feeling
                };

                LoggedUser?.AddActivity(climbActivity);
                LoggedUser?.SaveOrUpdate();

                Console.WriteLine("New climb activity created and saved.");

            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }


        void AddSwimActivity()
        {
            try
            {
                Console.WriteLine("Enter the total distance covered on the swim activity in M:");
                string distanceInput = Console.ReadLine();
                if (string.IsNullOrEmpty(distanceInput))
                    throw new ArgumentException("Please enter a valid distance.");

                if (!double.TryParse(distanceInput, out double distanceCovered) || distanceCovered <= 0)
                    throw new ArgumentException("Distance must be a valid positive number.");

                Console.WriteLine("Enter the total time spent on the swim activity in the format HH:MM:SS:");
                string timeTakenInput = Console.ReadLine();
                if (string.IsNullOrEmpty(timeTakenInput))
                    throw new ArgumentException("Please enter the valid time in the defined format.");

                if (!TimeSpan.TryParseExact(timeTakenInput, "hh\\:mm\\:ss", null, out TimeSpan timeTaken))
                    throw new FormatException("Time format is invalid.");

                Console.WriteLine("Enter the date of the swim activity in the format YYYY/MM/DD:");
                string dateOfActivityInput = Console.ReadLine();
                if (string.IsNullOrEmpty(dateOfActivityInput))
                    throw new ArgumentException("Please enter a valid date.");

                if (!DateTime.TryParseExact(dateOfActivityInput, "yyyy/MM/dd", null,
                    System.Globalization.DateTimeStyles.None, out DateTime dateOfActivity))
                    throw new FormatException("Date format is invalid.");

                Console.WriteLine("How did you feel after the swim activity?");
                Console.WriteLine("1. BAD");
                Console.WriteLine("2. OK");
                Console.WriteLine("3. GOOD");
                Console.WriteLine("4. STRONG");
                Console.WriteLine("5. VERY STRONG");
                string feelingInput = Console.ReadLine();

                if (!Enum.TryParse(feelingInput, out Feeling feeling) || !Enum.IsDefined(typeof(Feeling), feeling))
                    throw new ArgumentException("Invalid feeling selected.");

                // Create a new SwimActivity object
                SwimActivity swimActivity = new SwimActivity
                {
                    Distance = distanceCovered,
                    TimeTaken = timeTaken,
                    ActivityDate = dateOfActivity,
                    Feeling = feeling
                };

                LoggedUser?.AddActivity(swimActivity);
                LoggedUser?.SaveOrUpdate();

                Console.WriteLine("New swim activity created and saved.");

            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

    }



    void ListActivitiesByType()
    {
        Console.WriteLine("Select an activity type:");
        Console.WriteLine("1. Bike SportActivity");
        Console.WriteLine("2. Climb SportActivity");
        Console.WriteLine("3. Run SportActivity");
        Console.WriteLine("4. Swim SportActivity");

        string input = Console.ReadLine();

        if (int.TryParse(input, out int selectedType) && selectedType >= 1 && selectedType <= 4)
        {
            var selectedActivities = LoggedUser?.SportActivities.Where(a => (int)a.ActivityType == selectedType).ToList();

            if (selectedActivities.Any())
            {
                Console.WriteLine($"Activities for selected type:");

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

    void ListActivitiesByDate()
    {
        Console.WriteLine("Enter the date in YYYY/MM/DD format:");
        string dateInput = Console.ReadLine();

        if (DateTime.TryParseExact(dateInput, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime selectedDate))
        {
            var selectedActivities = LoggedUser?.SportActivities.Where(a => a.ActivityDate.Date == selectedDate.Date).ToList();

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



    public void ViewAllActivities()
    {
        if (LoggedUser != null) 
        {
            var activities = LoggedUser.SportActivities;

            if(activities != null && activities.Any())
            {
                Console.WriteLine("All activities");

                foreach (var activity in activities)
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
                Console.WriteLine("No activities found.");
            }
        }
        else
        {
            Console.WriteLine("Please Log in");
        }
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }


    void UpdateActivityDetails()
    {
        Console.WriteLine("Enter the ID of the activity to update:");
        string activityIdInput = Console.ReadLine();

        if (Guid.TryParse(activityIdInput, out Guid activityId))
        {
            var activityToUpdate = LoggedUser?.SportActivities.FirstOrDefault(a => a.Id == activityId);

            if (activityToUpdate != null)
            {
                UpdateActivityFields(activityToUpdate);
                LoggedUser?.SaveOrUpdate();

                Console.WriteLine("Activity details updated successfully!");
            }
            else
            {
                Console.WriteLine("Activity not found. Please enter a valid ID.");
                
            }
        }
        else
        {
            Console.WriteLine("Invalid activity ID. Please enter a valid integer.");

        }
    }

    void UpdateActivityFields(SportActivity activity)
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


    void DeleteActivityById()
    {
        var activityList = _dbContext.SportActivities.ToList();

        if (!activityList.Any())
        {
            Console.WriteLine("No activities found to delete.");
            return;
        }

        Console.WriteLine("Existing Activities:");
        foreach (var activity in activityList)
        {
            Console.WriteLine($"Activity ID: {activity.Id}");
            Console.WriteLine($"Activity Type: {activity.ActivityType}");
            Console.WriteLine($"Distance: {activity.Distance}");
            Console.WriteLine($"Time Taken: {activity.TimeTaken}");
            Console.WriteLine($"Activity Date: {activity.ActivityDate}");
            Console.WriteLine("-------------------------------------------------");
        }

        Console.WriteLine("Enter the ID of the activity to delete:");
        string activityIdInput = Console.ReadLine();

        if (Guid.TryParse(activityIdInput, out Guid activityId))
        {
            var activityToDelete = LoggedUser?.SportActivities.FirstOrDefault(a => a.Id == activityId);

            if (activityToDelete != null)
            {
                /*LoggedUser?.SportActivities.Remove(activityToDelete);
                LoggedUser?.SaveOrUpdate();
                Console.WriteLine("Activity deleted successfully.");*/

                // Remove associated sensor data
                var sensorDataList = _dbContext.SensorDatas
                    .Where(sd => sd.SportActivityId == activityId)
                    .ToList();

                _dbContext.SensorDatas.RemoveRange(sensorDataList);
                _dbContext.SportActivities.Remove(activityToDelete);
                _dbContext.SaveChanges();
                Console.WriteLine($"Activity with ID {activityId} and its related sensor data deleted successfully.");
            }
            else
            {
                Console.WriteLine("No activity found with the provided ID.");
            }
        }
        else
        {
            Console.WriteLine("Invalid activity ID. Please enter a valid GUID.");
        }
    }

    void RemoveAllActivities()
    {
        Console.WriteLine("Are you sure you want to delete all recorded activities? (Y/N)");
        string confirmation = Console.ReadLine();

        if (confirmation?.ToUpper() == "Y" || confirmation?.ToUpper() == "YES")
        {
            LoggedUser.RemoveAllActivities(LoggedUser.Id);

            Console.WriteLine("All activities deleted successful");
        }
        else
        {
            Console.WriteLine("Operation cancelled. No activities were deleted.");
        }

        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }

}