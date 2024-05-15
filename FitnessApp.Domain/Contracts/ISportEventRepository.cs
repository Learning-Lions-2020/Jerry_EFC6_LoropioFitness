using System.Collections.Generic;
using FitnessApp.Domain.Entities;
using FitnessApp.Domain.Entitities;
using FitnessApp.Domain.Entitities.Base;

namespace FitnessApp.Domain.Contracts
{
    public interface ISportEventRepository
    {
        public SportEvent GetSportEventById(int eventId);
        public List<SportEvent> GetAllSportEvents();
        public List<SportEvent> GetMySportEvents(User user);
        public SportEvent Save(SportEvent sportEvent);
    }
}

