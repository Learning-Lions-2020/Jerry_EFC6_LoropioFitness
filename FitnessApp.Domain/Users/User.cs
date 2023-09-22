using FitnessApp.Domain.Activities;

namespace FitnessApp.Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public List<RunActivity> RunActivities { get; set; }
        public List<BikeActivity> BikeActivities { get; set; }
        public List<SwimActivity> SwimActivities { get; set; }
        public List<ClimbActivity> ClimbActivities { get; set; }

        public User()
        {
            RunActivities = new List<RunActivity>();
            BikeActivities = new List<BikeActivity>();
            SwimActivities = new List<SwimActivity>();
            ClimbActivities = new List<ClimbActivity>();
        }
    }
}
