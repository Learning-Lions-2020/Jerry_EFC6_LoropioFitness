using FitnessApp.Data;
using FitnessApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

public class SportActivity
{
    FitnessAppContext fitnessAppContext = new FitnessAppContext();

    public User? FindUserById(int userId)
    {
        return fitnessAppContext.Users.Find(userId);
    }

    public void PrintUserWithId(int id)
    {
        User? user = FindUserById(id);

        if (user != null)
        {
            Console.WriteLine("Found User!");
            Console.WriteLine($"User Details: \nUserId: {user.Id}, First Name: {user.FirstName}, " +
                $"Last Name: {user.LastName}\n");
        }
        else
        {
            Console.WriteLine($"User with UserId {id} not found.");
        }
    }

    public void GetUserAndActivities(int userId)
    {
        var user = fitnessAppContext.Users
            .Include(user => user.RunActivities)
            .FirstOrDefault(user => user.Id == userId);

        if (user != null)
        {
            Console.WriteLine($"First Name: {user.FirstName} Last Name: {user.LastName}");
            foreach (var activity in user.RunActivities)
            {
                Console.WriteLine($"Name of Activity: {activity.Name} Distance: {activity.Distance}");
            }
        }
        else
        {
            Console.WriteLine("User not found or no activities recorded yet.");
        }
    }
}