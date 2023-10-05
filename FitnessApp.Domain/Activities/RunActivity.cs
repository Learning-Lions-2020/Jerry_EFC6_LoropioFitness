using FitnessApp.Domain.Users;

namespace FitnessApp.Domain.Activities
{
    public class RunActivity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Distance { get; set; }

        //foreign key to associate with user
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
