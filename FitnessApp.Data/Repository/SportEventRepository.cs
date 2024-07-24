using System.Collections.Generic;
using System.Linq;
using FitnessApp.Data.DBContext;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entities;
using FitnessApp.Domain.Entities.Base;
using FitnessApp.Domain.Entitities;

namespace FitnessApp.Data.Repository
{
    public class SportEventRepository : ISportEventRepository
    {
        private FitnessAppContext _dbContext;

        public SportEventRepository(FitnessAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SportEvent GetSportEventById(int eventId)
        {
            return _dbContext.SportEvents.Find(eventId);
        }

        public List<SportEvent> GetAllSportEvents()
        {
            return _dbContext.SportEvents.ToList();
        }

        public List<SportEvent> GetMySportEvents(User user)
        {
            return _dbContext.SportEvents
                             .Where(ue => ue.Users.Contains(user))
                             .ToList();
        }

        public SportEvent Save(SportEvent sportEvent)
        {
            _dbContext.SportEvents.Add(sportEvent);
            _dbContext.SaveChanges();

            return sportEvent;
        }
    }
}



