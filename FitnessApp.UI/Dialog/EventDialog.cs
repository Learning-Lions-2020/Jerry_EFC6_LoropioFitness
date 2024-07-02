using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entitities;
using FitnessApp.Domain.Entitities.Base;

namespace FitnessApp.UI.Dialog
{
    public class EventDialog : BaseDialog
    {
        private ISportEventService _sportEventService;

       public EventDialog(User user, ISportEventService sportEventService) : base(user) 
        { 
            _sportEventService = sportEventService; 
        }
        public void ManageSportEvents()
        {
            do
            {
                // Sport events management logic
                Console.WriteLine("Sport events management logic...");
                Console.WriteLine("1. Add a new sports event");
                Console.WriteLine("2. List All sports events");
                Console.WriteLine("3. Register for a sports event");
                Console.WriteLine("4. List My sports events");
                Console.WriteLine("5. Unregister from a sport events");
                Console.WriteLine("6. Back");
                Console.WriteLine("7. Log out");

                var authenticationDialog = new AuthenticationDialog(ServiceProvider);

                // Get user input
                string input = Console.ReadLine();

                // Process user input
                switch (input)
                {
                    case "1":
                        AddSportsEvent();
                        break;
                    case "2":
                        GetSportsEvents();
                        break;
                    case "3":
                        RegisterForSportsEvent();
                        break;
                    case "4":
                        GetMySportsEvents();
                        break;
                    case "5":
                        UnregisterFromSportsEvent();
                        break;
                    case "6":
                        authenticationDialog.Menu();
                        break;
                    case "7":
                        authenticationDialog.Logout();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        ManageSportEvents();
                        break;
                }
            } while (true);
        }

    public void AddSportsEvent()
        {
            Console.WriteLine("Adding a new sports event:");

            try
            {
                var sportEvent = new SportEvent();

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

                _sportEventService.Save(sportEvent);
               

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
            GetSportsEvents(false); 

            Console.WriteLine("Enter the ID of the event you wish to register for:");
            if (!int.TryParse(Console.ReadLine(), out int eventId))
            {
                Console.WriteLine("Invalid event ID. Please enter a valid integer.");
                return;
            }

            var sportEvent = _sportEventService.GetSportEventById(eventId);
            if (sportEvent != null)
            {
                bool isRegistered = false;
                foreach (var registeredEvent in LoggedUser.SportEvent)
                {
                    if (registeredEvent.Id == sportEvent.Id)
                    {
                        isRegistered = true;
                        break;
                    }
                }

                if (isRegistered)
                {
                    Console.WriteLine("You are already registered for this event.");
                }
                else
                {
                    LoggedUser.SportEvent.Add(sportEvent);
                    LoggedUser.SaveOrUpdate();
                    Console.WriteLine("Registered successfully for the selected event.");
                }
            }
            else
            {
                Console.WriteLine("Event not found.");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadLine();

        }

        public void GetSportsEvents(bool returnToMain = true)
        {
            try
            {
                Console.WriteLine("List of Sports Events:");

                var events = _sportEventService.GetSportsEvents();

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


        public void GetMySportsEvents(bool returnToMain = true)
        {
            try
            {


                if (LoggedUser != null)
                {
                    var sportEvents = _sportEventService.GetMySportsEvents(LoggedUser);

                    if(sportEvents != null)
                    {
                        Console.WriteLine("List of my sportevents ");

                        foreach (var sportEvent in sportEvents)
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
                GetMySportsEvents(false); 


                if (LoggedUser.SportEvent.Count == 0)
                {
                    Console.WriteLine("You are not registered for any events.");
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("Enter the Event ID from which you wish to unregister:");

                if (!int.TryParse(Console.ReadLine(), out int eventId))
                {
                    Console.WriteLine("Invalid input. Please select a valid Event ID.");
                    Console.ReadLine();
                    return;
                }

                var sportEvent = _sportEventService.GetSportEventById(eventId);

                if (sportEvent == null)
                {
                    Console.WriteLine("Event not found.");
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadLine();
                    return;
                }

                bool isRegistered = false;
                foreach (var registeredEvent in LoggedUser.SportEvent)
                {
                    if (registeredEvent.Id == sportEvent.Id)
                    {
                        isRegistered = true;
                        LoggedUser.SportEvent.Remove(registeredEvent);
                        LoggedUser.SaveOrUpdate();

                        Console.WriteLine($"Unregistered successfully from {sportEvent.Name}.");
                        break;
                    }
                }

                if (!isRegistered)
                {
                    Console.WriteLine("You are not registered for this event.");
                }

                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadLine();
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
