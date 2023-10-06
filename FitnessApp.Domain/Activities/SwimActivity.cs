using FitnessApp.Domain.Users;

namespace FitnessApp.Domain.Activities
{
    public class SwimActivity : ISportActivity
    {
        public new int Id { get; set; }
        public new string? Name { get; set; }
        public new double Distance { get; set; }

        //foreign key to associate with user
        public new int? UserId { get; set; }
    }
}
