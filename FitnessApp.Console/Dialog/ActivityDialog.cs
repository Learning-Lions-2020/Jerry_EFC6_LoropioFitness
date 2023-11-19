using System.ComponentModel.DataAnnotations;
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
    }

    private void OpenActivityDialog(ActivityType activityType)
    {
        switch (activityType)
        {
            case ActivityType.RunActivity:
                AddRunActivity();
                break;
        }
        // Task 7 : add the code to add a Bike Activity
    }

    private void AddRunActivity()
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
            }
            Console.WriteLine("New run Activity created and saved.");
        }

        // Task 9 : add further exceptions to account for conversion and format problems
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }

    }
}