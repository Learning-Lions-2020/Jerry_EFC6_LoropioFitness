using FitnessApp.Data;
using FitnessApp.Domain;
using FitnessApp.Console.SetupDb;


var dbSeeder = new DbSeeder();
dbSeeder.CreateTestData();

FitnessAppContext fitnessAppContext = new FitnessAppContext();

Console.WriteLine("Enter User's first name to find: ");
string? firstNameToFind = Console.ReadLine();
    
Console.WriteLine("Enter the new First Name: ");
string? newFirstName = Console.ReadLine();
    
FindUserByFirstName(firstNameToFind);
UpdateUserFirstName(firstNameToFind, newFirstName);

void FindUserByFirstName(string? firstName)
{
    User? user = fitnessAppContext.Users.FirstOrDefault(u => u.FirstName == firstName);

    if (user != null)
    {
        Console.WriteLine($"User FirstName: {user.FirstName} found!");
    }
    else
    {
        Console.WriteLine($"User with FirstName '{firstName}' not found.");
    }
}

void UpdateUserFirstName(string? fromFirstName, string? toFirstName)
{
    User user = fitnessAppContext.Users.FirstOrDefault(u => u.FirstName == fromFirstName);

    if (user != null)
    {
        Console.WriteLine($"User Details before update: FirstName: {user.FirstName}");
        if (toFirstName != null) user.FirstName = toFirstName;

        Console.WriteLine("Before calling DetectChanges: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);
        fitnessAppContext.ChangeTracker.DetectChanges();
        Console.WriteLine("After calling DetectChanges: " + fitnessAppContext.ChangeTracker.DebugView.ShortView);

        fitnessAppContext.SaveChanges();

        Console.WriteLine($"User Details After Update: FirstName: {user.FirstName}");
    }
    else
    {
        Console.WriteLine("User not found for updating FirstName.");
    }
}

