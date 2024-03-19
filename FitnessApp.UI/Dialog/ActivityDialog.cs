using FitnessApp.Domain.CustomTypes;
using FitnessApp.Domain.Entitities;

namespace FitnessApp.UI.Dialog;

internal class ActivityDialog
{
    private int _userId;
    public void SetUserId(int userId)
    {
        this._userId = userId;
    }

    public void EnterActivity()
    {
        // Task 6: Give the possibility to select and enter a activity

        string[] validActivityTypeInput = new string[4] { "1", "2", "3", "4" };

        Console.WriteLine("What type of sports activity do you want to enter ?");
        Console.WriteLine("1. Bike SportActivity");
        Console.WriteLine("2. Climb SportActivity");
        Console.WriteLine("3. Run SportActivity");
        Console.WriteLine("4. Swim SportActivity");

        string activityTypeInput = Console.ReadLine();


        ActivityType activityType;


        if (activityTypeInput == null)
        {
            return;
        }

        activityType = (ActivityType)(int.Parse(activityTypeInput));

        OpenActivityDialog(activityType);
    }

    private void OpenActivityDialog(ActivityType activityType)
    {
        switch (activityType)
        {
            case ActivityType.RunActivity:
                AddRunActivity();
                break;
            case ActivityType.BikeActivity:
                AddBikeActivity();
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

                    // Add the run activity to the user's activities
                    var user = new User().GetUser(_userId);
                    if (user != null)
                    {
                        user.AddActivity(runActivity);
                        user.SaveOrUpdate();
                    }
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

                // Add the bike activity to the user's activities
                User user = new User();
                user.AddActivity(bikeActivity);

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

    }
}