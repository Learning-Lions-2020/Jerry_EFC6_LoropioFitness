using FitnessApp.Data;
using FitnessApp.Domain;
using FitnessApp.Console.SetupDb;


var dbSeeder = new DbSeeder();
dbSeeder.CreateTestData();


PrintUserWithId(3);

void PrintUserWithId(int id)
{
    User foundUser = FindUserById(id);

    if (foundUser != null)
    {
        Console.WriteLine("Found User!");
        Console.WriteLine($"User Details: UserId: {foundUser.Id}, First Name: {foundUser.FirstName}, Last Name: {foundUser.LastName}");
    }
    else
    {
        Console.WriteLine($"User with UserId {id} not found.");
    }
}


User FindUserById(int userId)
{
    FitnessAppContext fitnessAppContext = new FitnessAppContext();

    return fitnessAppContext.Users.Find(userId);
}

