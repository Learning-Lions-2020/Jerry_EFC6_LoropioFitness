using FitnessApp.Data;
using FitnessApp.Domain;
using FitnessApp.Console.SetupDb;


Dialog();
var dbSeeder = new DbSeeder();
dbSeeder.CreateTestData();

void Dialog()
{
    Console.WriteLine("Enter User's first name to find: ");
    string? firstNameToFind = Console.ReadLine();
    Console.WriteLine("Enter the new First Name: ");
    string? newFirstName = Console.ReadLine();
    User? user = FindUserById(id);

    FindUserByFirstName(firstNameToFind);
    UpdateUserFirstName(firstNameToFind, newFirstName);
}


void FindUserByFirstName(string firstName)
{
    FitnessAppContext fitnessAppContext = new FitnessAppContext();

    User user = fitnessAppContext.Users.FirstOrDefault(u => u.FirstName == firstName);

    if (user != null)
    {
        Console.WriteLine($"User FirstName: {user.FirstName} found!");
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
        Console.WriteLine("Before: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);

        user.FirstName = newFirstName;
        fitnessAppContext.SaveChanges();

        Console.WriteLine($"User Details After Update: FirstName: {user.FirstName}");
        fitnessAppContext.ChangeTracker.DetectChanges();
        Console.WriteLine("After: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);
    }
    else
    {
        Console.WriteLine("User not found for updating FirstName.");
    }
}

