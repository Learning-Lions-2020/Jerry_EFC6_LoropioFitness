using FitnessApp.Data;
using FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;

using (FitnessAppContext context = new FitnessAppContext())
{
    context.Database.EnsureCreated();
}

GetActivities("Bonface");

Console.WriteLine("User and thier activities displayed");


void GetActivities(string userName)
{
    using var context = new FitnessAppContext();

    var users = context.Users
        .Include(user => user.RunActivities)
        .Where(user => 
            user.FirstName.Contains(userName) ||
            user.LastName.Contains(userName))
        .ToList();

    if (users.Any())
        foreach (var user in users)
        {
            Console.WriteLine($"First Name: {user.FirstName} Last Name: {user.LastName}");
            foreach (var activity in user.RunActivities)
            {
                Console.WriteLine($"Name of Activity: {activity.Name} Distance: {activity.Distance}");
            }
        }
    else
        Console.WriteLine("No activities recorded yet");

}