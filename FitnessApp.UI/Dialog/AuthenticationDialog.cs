using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entitities;
using FitnessApp.UI.Dialog;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.UI.Dialog
{
    public class AuthenticationDialog : BaseDialog
    {

        public AuthenticationDialog(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public void Start()
        {
            // Display options to create a new user account, log in, or quit
            Console.WriteLine("Welcome to Loropio Fitness App!");
            Console.WriteLine("1. Sign up");
            Console.WriteLine("2. Log in");
            Console.WriteLine("3. Quit");

            // Get user input
            string input = Console.ReadLine();

            // Process user input
            switch (input)
            {
                case "1":
                    Signup();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Start();
                    break;
            }
        }

        public void Signup()
        {
            // Task 2:Create a Dialog for User to Register with his UserName and his Password

            Console.WriteLine("Enter your username:");
            string userNameInput = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            string passwordInput = Console.ReadLine();

            // Task 3: Uncomment the lines below and make the work. Use the already implemented Register method for the User

            if (!string.IsNullOrEmpty(userNameInput) && !string.IsNullOrEmpty(passwordInput))
            {
                GetUser()?.Register(userNameInput, passwordInput);
                Console.WriteLine("User registered successfully!");
                Menu();
            }
            else
            {
                Console.WriteLine("You did not enter valid credentials !");
            }
        }

        private void Login()
        {
            // Task 4: Create the Dialog to Logon with Username and Password

            Console.WriteLine("Enter your username:");
            string userNameInput = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            string passwordInput = Console.ReadLine();

            // uncomment the lines below and make the work

            if (!string.IsNullOrEmpty(userNameInput) && !string.IsNullOrEmpty(passwordInput))
            {
                User user = GetUser();

                LoggedUser = user.GetUser(userNameInput, passwordInput);

                if (LoggedUser != null)
                {
                    Console.WriteLine($"Welcome {LoggedUser.UserName}, you have logged on successfully !");
                    Menu();
                }
                else
                {
                    Console.WriteLine("You did not enter valid credentials !");
                    Start();
                }
            }
            else
            {
                Console.WriteLine("You did not provide your username or Password !");
            }

        }

        public void Logout()
        {
            Console.WriteLine("You have successfully logged out!");
            Environment.Exit(0);

        }

        public void Menu()
        {
            Console.WriteLine("Welcome to Loropio Fitness App!");
            Console.WriteLine("1. Manage Sport Activities");
            Console.WriteLine("2. Manage Sport Events");
            Console.WriteLine("3. Manage Profile");
            Console.WriteLine("4. Log out");
            Console.WriteLine("5. Quit");

            // Get user input
            string input = Console.ReadLine();

            var activityDialog = new ActivityDialog(LoggedUser);
            var eventService = ServiceProvider.GetRequiredService<ISportEventService>();    
            var eventDialog = new EventDialog(LoggedUser, eventService);
            var userDialog = new UserDialog(LoggedUser);

            // Process user input
            switch (input)
            {
                case "1":
                    activityDialog.ManageSportActivities();
                    break;
                case "2":
                    eventDialog.ManageSportEvents();
                    break;
                case "3":
                    userDialog.ManageProfile();
                    break;
                case "4":
                    Logout();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Start();
                    break;
            }
        }
    }

}


