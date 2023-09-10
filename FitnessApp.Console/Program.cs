using FitnessApp.Data;
using FitnessApp.Domain;
using FitnessApp.Console.SetupDb;


var dbSeeder = new DbSeeder();
dbSeeder.CreateTestData();


PrintUserWithId(3);

void PrintUserWithId(int id)
{
    User? user = FindUserById(id);

    if (user != null)
    {
        Console.WriteLine("Found User!");
        Console.WriteLine($"User Details: UserId: {user.Id}, First Name: {user.FirstName}, Last Name: {user.LastName}");
    }
    else
    {
        Console.WriteLine($"User with UserId {id} not found.");
    }
}


User? FindUserById(int userId)
{
    FitnessAppContext fitnessAppContext = new FitnessAppContext();

    return fitnessAppContext.Users.Find(userId);
}

