using FitnessApp.Domain.Entitities.Base;
using FitnessApp.Domain.Entitities;

namespace FitnessApp.Domain.Entities
{
    public class UserSportEvent
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int SportEventId { get; set; }
        public SportEvent SportEvent { get; set; }
    }
}

