using FitnessApp.Domain.Contracts;
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

        public ICollection<User> Users { get; set; } = new List<User>();

        public void SaveEvent()
        {
            _sportEventRepository.Save(this);
        }
    }

}
