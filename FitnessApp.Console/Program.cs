using FitnessApp.Data;
using FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;

using (FitnessAppContext context = new FitnessAppContext())
{
    context.Database.EnsureCreated();
}

AddUsersWithActivities();
GetActivities();

Console.WriteLine("DB was created and run activities displyed");

void AddUsersWithActivities()
{
    User user1 = AddUser("Mike", "Miller");
    AddRunActivity(user1, "Run from Loropio to Lodwar", 40);
    AddRunActivity(user1, "Run from Nairobi to Nakuru", 80);


    User user2 = AddUser("Joan", "Henson");
    AddRunActivity(user2, "Run from Maralal to Wamba", 60);
    AddRunActivity(user2, "Run from Naivasha to Nakuru", 40);

    var context = new FitnessAppContext();

    context.Users.Add(user1);
    context.Users.Add(user2);


    context.SaveChanges();
}

void AddRunActivity(User user, string nameRunActivity, int distance)
{
    user.RunActivities.Add(new RunActivity { Name = nameRunActivity, Distance = distance });
}

void GetActivities()
{
    using var context = new FitnessAppContext();

    var users = context.Users.Include(user => user.RunActivities).ToList();
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

static User AddUser(string firstName, string lastName)
{
    var user1 = new User()
    {
        FirstName = firstName,
        LastName = lastName
    };

    return user1;
}