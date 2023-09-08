using FitnessApp.Data;
using FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using (FitnessAppContext context = new FitnessAppContext())
{
    context.Database.EnsureCreated();
}

PrintUser();

void PrintUser()
{
    int userIdToFind = 20;
    User foundUser = FindUserById(userIdToFind);

    if (foundUser != null)
    {
        Console.WriteLine("Found User!");
        Console.WriteLine($"User Details: UserId: {foundUser.Id}, First Name: {foundUser.FirstName}, Last Name: {foundUser.LastName}");
    }
    else
    {
        Console.WriteLine($"User with UserId {userIdToFind} not found.");
    }
}


User FindUserById(int userId)
{
    FitnessAppContext fitnessAppContext = new FitnessAppContext();

    return fitnessAppContext.Users.Find(userId);
}

