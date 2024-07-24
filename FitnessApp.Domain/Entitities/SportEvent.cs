using FitnessApp.Domain.Contracts;

namespace FitnessApp.Domain.Entitities
{
    public class SportEvent
    {
        private readonly ISportEventRepository? _sportEventRepository;

        public SportEvent() { }

        public SportEvent(ISportEventRepository? sportEventRepository)
        {

            _sportEventRepository = sportEventRepository;
        }

        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

    }

}
