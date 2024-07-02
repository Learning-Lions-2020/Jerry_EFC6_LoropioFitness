using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entities;
using System;

namespace FitnessApp.Domain.Entitities.Base
{
    public class SportEvent
    {
        private static ISportEventRepository _sportEventRepository;

        public SportEvent() { }
        public SportEvent(ISportEventRepository sportEventRepository) { 
        
            _sportEventRepository = sportEventRepository;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // Add UserId property
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<UserSportEvent> UserSportEvents { get; set; }

    }

}
