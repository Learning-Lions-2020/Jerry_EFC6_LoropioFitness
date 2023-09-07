using FitnessApp.Data;
using FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;

using (FitnessAppContext context = new FitnessAppContext())
{
    context.Database.EnsureCreated();
}

AddUsersWithActivities();
GetActivities();

Console.WriteLine("Users and their activities added and displayed");

void AddUsersWithActivities()
{
    User bonface = AddUser("Bonface", "Njuguna");
    AddRunActivity(bonface, "Run from Kalokol to Lodwar", 60);
    AddRunActivity(bonface, "Run from Naivasha to Nakuru", 40);


    User omondi = AddUser("Omondi", "Wyclife");
    AddRunActivity(omondi, "Run from Homabay to Kisumu", 100);
    AddRunActivity(omondi, "Run from Siaya to Ugenya", 70);

    User ayako = AddUser("Tabby", "Ayako");
    AddRunActivity(ayako, "Run from Kajiado to Maasai Mara", 50);
    AddRunActivity(ayako, "Run from Lakipia to Namanga", 90);

    User isaya = AddUser("Isaya", "Mutekhele");
    AddRunActivity(isaya, "Run from Bungoma to Kitale", 60);
    AddRunActivity(isaya, "Run from Vihiga to Shimakoko", 30);

    User mbula = AddUser("Mbula", "Irine");
    AddRunActivity(omondi, "Run from Machakos to Wetu", 80);
    AddRunActivity(omondi, "Run from Konza to Machakos", 80);

    User cheptoo = AddUser("Ruto", "Cheptoo");
    AddRunActivity(omondi, "Run from Eldoret to Langas", 30);
    AddRunActivity(omondi, "Run from Sugoi to Ravine", 50);

    var context = new FitnessAppContext();

    context.Users.Add(bonface);
    context.Users.Add(omondi);
    context.Users.Add(ayako);
    context.Users.Add(isaya);
    context.Users.Add(mbula);
    context.Users.Add(cheptoo);


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