using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entitities.Base;
using FitnessApp.Domain.Entitities;

namespace FitnessApp.Domain.Services
{
    public class SportEventService : ISportEventService
    {
        private static ISportEventRepository _sportEventRepository;

        public SportEventService(ISportEventRepository sportEventRepository)
        {
            _sportEventRepository = sportEventRepository;
        }

        public void Save(SportEvent sportEvent)
        {
            _sportEventRepository.Save(sportEvent);
        }

        public List<SportEvent> GetSportsEvents()
        {
            return _sportEventRepository.GetAllSportEvents();
        }

        public SportEvent? GetSportEventById(int sportEventId)
        {
            return _sportEventRepository.GetSportEventById(sportEventId);
        }

        public List<SportEvent> GetMySportsEvents(User user)
        {
            return _sportEventRepository.GetMySportEvents(user);    
        }

    }
}
