using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using FitnessApp.Domain.CustomTypes;
using FitnessApp.Domain.Entities;
using FitnessApp.Domain.Entities.Base;
using FitnessApp.Domain.Entitities;

namespace FitnessApp.UI.Dialog
{
    internal class ActivityDialog
    {
        private int _userId;
        public void SetUserId(int userId)
        {
            this._userId = userId;
        }

        public void EnterActivity()
        {
            try
            {
                string[] validActivityTypeInput = new string[4] { "1", "2", "3", "4" };

                Console.WriteLine("What type of sports activity do you want to enter ?");
                Console.WriteLine("1. Bike SportActivity");
                Console.WriteLine("2. Climb SportActivity");
                Console.WriteLine("3. Run SportActivity");
                Console.WriteLine("4. Swim SportActivity");

                string activityTypeInput = Console.ReadLine();

                if (activityTypeInput == null)
                {
                    return;
                }

                var activityType = (ActivityType)(int.Parse(activityTypeInput));

                OpenActivityDialog(activityType);
            }
            catch (ValidationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void OpenActivityDialog(ActivityType activityType)
        {
            switch (activityType)
            {
                case ActivityType.RunActivity:
                    AddRunActivity();
                    break;
            }

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

                        var runActivity = new RunActivity()
                        {
                            ActivityDate = dateOfActivity,
                            Distance = distanceCovered,
                            Feeling = feeling,
                            TimeTaken = timeTaken
                        };
                        
                        var user = new User().GetUser(_userId);
                        
                        if (user != null)
                        {
                            user.AddActivity(runActivity);
                            user.SaveOrUpdate();
                        }
                    }
                    Console.WriteLine("New run Activity created and saved.");
                }
                catch (FormatException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}. Please enter a valid number.");
                    Console.ResetColor();
                }
                catch (ArgumentNullException ex)
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
}
