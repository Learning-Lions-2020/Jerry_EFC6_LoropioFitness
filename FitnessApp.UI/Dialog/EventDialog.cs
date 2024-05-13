using System;
using System.Linq;
using FitnessApp.Data.Repository;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entities;
using FitnessApp.Domain.Entitities;
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
            _userRepository = userRepository; // Initialize the _userRepository field
            _userId = userId;
        }

        public EventDialog()
        {
        }

        /*public void SetUserId(int userId)
{
   this._userId = userId;
}*/

        public void AddSportsEvent()
        {
            Console.WriteLine("Adding a new sports event:");

            try
            {
                var sportEvent = new SportEvent(_sportEventRepository);

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


                sportEvent.Name = name;
                sportEvent.Description = description;
                sportEvent.Date = date;
                sportEvent.City = city;
                sportEvent.Country = country;

                sportEvent.SaveEvent();
               

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

            var user = new User().GetUser(_userId);

            if (user == null)
            {
                Console.WriteLine("User not found. Please log in again.");
                return;
            }
            else
            {
                user.SportEvent.Add(sportEvent);
                user.SaveOrUpdate();
            }

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


        public void ListMySportsEvents(bool returnToMain = true)
        {
            try
            {
                var user = _userRepository.GetUserById(_userId);
                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }

                Console.WriteLine($"Registered Sports Events for User ID {_userId}:");

                var registeredEvents = user.RegisteredEvents;
                if (registeredEvents.Any())
                {
                    foreach (var sportEvent in registeredEvents)
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
                    Console.WriteLine("No registered events found for this user.");
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

        public void UnregisterFromSportsEvent()
        {
            try
            {
                ListMySportsEvents(false); // Display the user's registered events

                var user = _userRepository.GetUserById(_userId);
                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }

                var registeredEvents = user.RegisteredEvents;
                if (!registeredEvents.Any())
                {
                    Console.WriteLine("You are not registered for any events.");
                    return;
                }

                Console.WriteLine("Enter the ID of the event you wish to unregister from:");
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

                if (registeredEvents.Contains(sportEvent))
                {
                    user.UnregisterFromEvent(sportEvent);
                    _userRepository.SaveOrUpdate();
                    Console.WriteLine("You have been successfully unregistered from the event.");
                }
                else
                {
                    Console.WriteLine("You are not registered for this event.");
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
