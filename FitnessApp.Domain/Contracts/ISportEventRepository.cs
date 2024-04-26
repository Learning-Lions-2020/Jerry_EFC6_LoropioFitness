using System.Collections.Generic;
using FitnessApp.Domain.Entities;
using FitnessApp.Domain.Entitities.Base;

namespace FitnessApp.Domain.Contracts
{
    public interface ISportEventRepository
    {
        SportEvent GetSportEventById(int eventId);
        IEnumerable<SportEvent> GetAllSportEvents();
        void AddSportEvent(SportEvent sportEvent);
        void Save(SportEvent sportEvent);
    }
}

