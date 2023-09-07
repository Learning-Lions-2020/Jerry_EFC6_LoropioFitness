using FitnessApp.Data;
using FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;

using (FitnessAppContext context = new FitnessAppContext())
{
    context.Database.EnsureCreated();
}

int pageNumber = 2;
int pageSize = 2;

GetActivities(pageNumber, pageSize);

Console.WriteLine("User and thier activities displayed");


void GetActivities(int pageNumber, int pageSize)
{
    using var context = new FitnessAppContext();

    int recordsToSkip = (pageNumber - 1) * pageSize;

    var users = context.Users
        .Include(user => user.RunActivities)
        .Skip(recordsToSkip)
        .Take(pageSize)
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