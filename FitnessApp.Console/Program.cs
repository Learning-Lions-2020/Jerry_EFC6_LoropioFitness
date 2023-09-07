using FitnessApp.Data;
using FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using (FitnessAppContext context = new FitnessAppContext())
{
    context.Database.EnsureCreated();
}


SortUsers();

Console.WriteLine("Sorted");


void SortUsers()
{
    FitnessAppContext fitnessAppContext = new FitnessAppContext();

    var usersByLastName = fitnessAppContext.Users
        .OrderBy(_ => _.FirstName)
        .ThenBy(_ => _.LastName)
        .ToList();

    usersByLastName.ForEach(_ => Console.WriteLine(_.FirstName + "," + _.LastName));

    var usersDescending = fitnessAppContext.Users
        .OrderByDescending(_ => _.LastName)
        .ThenByDescending(_ => _.FirstName)
        .ToList();
    Console.WriteLine("\n**Descending Last to First**");
    usersDescending.ForEach(_ => Console.WriteLine(_.LastName + "," + _.FirstName));
}