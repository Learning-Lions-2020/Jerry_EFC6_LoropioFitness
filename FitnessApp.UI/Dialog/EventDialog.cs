using System;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entitities;
using FitnessApp.Domain.Entitities.Base;

namespace FitnessApp.UI.Dialog
{
    public class EventDialog
    {
        private readonly ISportEventRepository _sportEventRepository;

        public EventDialog(ISportEventRepository sportEventRepository)
        {
            _sportEventRepository = sportEventRepository;
        }

        public void AddSportsEvent()
        {
            Console.WriteLine("Adding a new sports event:");

            try
            {
                Console.WriteLine("Enter the name of the event:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter the description of the event:");
                string description = Console.ReadLine();

                Console.WriteLine("Enter the date of the event (YYYY/MM/DD):");
                string dateInput = Console.ReadLine();
                if (!DateTime.TryParseExact(dateInput, "yyyy/MM/dd", null,
                    System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    throw new ArgumentException("Invalid date format. Please use YYYY/MM/DD format.");
                }

                Console.WriteLine("Enter the city where the event will take place:");
                string city = Console.ReadLine();

                Console.WriteLine("Enter the country where the event will take place:");
                string country = Console.ReadLine();

                // Create a new SportEvent object
                SportEvent sportEvent = new SportEvent
                {
                    Name = name,
                    Description = description,
                    Date = date,
                    City = city,
                    Country = country
                };

                // Save the event using the repository
                _sportEventRepository.Save(sportEvent);

                Console.WriteLine("Sports event added successfully!");
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}

