namespace FitnessApp.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public List<RunActivity> RunActivities { get; set; }

        public User()
        {
            RunActivities = new List<RunActivity>();
        }
    }
}
