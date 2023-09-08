using FitnessApp.Data;
using FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using (FitnessAppContext context = new FitnessAppContext())
{
    context.Database.EnsureCreated();
}

Dialog();

void Dialog()
{
    Console.WriteLine("Enter User's first name to find: ");
    string? firstNameToFind = Console.ReadLine();
    Console.WriteLine("Enter the new First Name: ");
    string? newFirstName = Console.ReadLine();

    FindUserByFirstName(firstNameToFind);
    UpdateUserFirstName(firstNameToFind, newFirstName);
}


void FindUserByFirstName(string firstName)
{
    FitnessAppContext fitnessAppContext = new FitnessAppContext();

    User user = fitnessAppContext.Users.FirstOrDefault(u => u.FirstName == firstName);

    if (user != null)
    {
        Console.WriteLine($"User Details Before Update: FirstName: {user.FirstName}");
    }
    else
    {
        Console.WriteLine($"User with FirstName '{firstName}' not found.");
    }
}

void UpdateUserFirstName(string currentFirstName, string newFirstName)
{
    FitnessAppContext fitnessAppContext = new FitnessAppContext();

    User user = fitnessAppContext.Users.FirstOrDefault(u => u.FirstName == currentFirstName);

    if (user != null)
    {
        Console.WriteLine($"User Details After Update: FirstName: {user.FirstName}");
        user.FirstName = newFirstName;

        fitnessAppContext.ChangeTracker.DetectChanges();
        fitnessAppContext.SaveChanges();

        Console.WriteLine($"User Details After Update: FirstName: {user.FirstName}");
    }
    else
    {
        Console.WriteLine("User not found for updating FirstName.");
    }
}

