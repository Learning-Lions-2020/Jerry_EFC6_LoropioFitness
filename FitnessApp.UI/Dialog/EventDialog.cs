using System;
using System.Linq;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entities;
using FitnessApp.Domain.Entitities.Base;

namespace FitnessApp.UI.Dialog
{
    public class EventDialog
    {
        private readonly ISportEventRepository _sportEventRepository;
        private readonly IUserRepository _userRepository;
        private readonly int _userId;

        public EventDialog(ISportEventRepository sportEventRepository, IUserRepository userRepository, int userId)
        {
            _sportEventRepository = sportEventRepository;
            _userRepository = userRepository;
            _userId = userId;
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
                if (!DateTime.TryParse(dateInput, out DateTime date))
                {
                    Console.WriteLine("Invalid date format. Please enter the date in YYYY/MM/DD format.");
                    return;
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
                _sportEventRepository.AddSportEvent(sportEvent);
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


        public void RegisterForSportsEvent()
        {
            Console.WriteLine("Available Sports Events:");
            ListSportsEvents(false); // Display available events without prompting for return to main menu

            Console.WriteLine("Enter the ID of the event you wish to register for:");
            if (!int.TryParse(Console.ReadLine(), out int eventId))
            {
                Console.WriteLine("Invalid event ID. Please enter a valid integer.");
                return;
            }

            var sportEvent = _sportEventRepository.GetSportEventById(eventId);
            if (sportEvent == null)
            {
                Console.WriteLine("Event not found. Please enter a valid event ID.");
                return;
            }

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                Console.WriteLine("User not found. Please log in again.");
                return;
            }

            // Check if the user is already registered for the event
            if (user.RegisteredEvents.Any(e => e.Id == eventId))
            {
                Console.WriteLine("You are already registered for this event.");
                return;
            }

            // Register the user for the event
            user.RegisterForEvent(sportEvent);
            _userRepository.SaveOrUpdate();

            Console.WriteLine("Registration successful!");
        }

        public void ListSportsEvents(bool returnToMain = true)
        {
            try
            {
                Console.WriteLine("List of Sports Events:");

                var events = _sportEventRepository.GetAllSportEvents();

                if (events.Any())
                {
                    foreach (var sportEvent in events)
                    {
                        Console.WriteLine($"ID: {sportEvent.Id}");
                        Console.WriteLine($"Name: {sportEvent.Name}");
                        Console.WriteLine($"Description: {sportEvent.Description}");
                        Console.WriteLine($"Date: {sportEvent.Date:yyyy/MM/dd}");
                        Console.WriteLine($"City: {sportEvent.City}");
                        Console.WriteLine($"Country: {sportEvent.Country}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No events found.");
                }

                if (returnToMain)
                {
                    Console.WriteLine("Press any key to return to the main menu...");
                    Console.ReadKey();
                }
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
