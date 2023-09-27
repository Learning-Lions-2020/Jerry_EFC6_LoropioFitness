using FitnessApp.Domain.Activities;

namespace FitnessApp.Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public List<BikeActivity> BikeActivities { get; } = new List<BikeActivity>();
        public List<ClimbActivity> ClimbActivities { get; } = new List<ClimbActivity>();
        public List<RunActivity> RunActivities { get; } = new List<RunActivity>();
        public List<SwimActivity> SwimActivities { get; } = new List<SwimActivity>();

        public void AddBikeActivity(string name, double distance)
        {
            BikeActivities.Add(new BikeActivity { Name = name, Distance = distance });
        }

        public void AddClimbActivity(string name, double distance)
        {
            ClimbActivities.Add(new ClimbActivity { Name = name, Distance = distance });
        }

        public void AddRunActivity(string name, double distance)
        {
            RunActivities.Add(new RunActivity { Name = name, Distance = distance });
        }

        public void AddSwimActivity(string name, double distance)
        {
            SwimActivities.Add(new SwimActivity { Name = name, Distance = distance });
        }
    }
}
