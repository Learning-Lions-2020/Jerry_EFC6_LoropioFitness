using FitnessApp.Domain.Entitities.Base;
using FitnessApp.Domain.Entitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Domain.Contracts
{
    public interface ISportEventService
    {
        public void Save(SportEvent sportEvent);

        public List<SportEvent> GetSportsEvents();

        public SportEvent? GetSportEventById(int sportEventId);

        public List<SportEvent> GetMySportsEvents(User user);
    }
}
