using System.Collections.Generic;
using FitnessApp.Data.DBContext;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entities;
using FitnessApp.Domain.Entitities;
using FitnessApp.Domain.Entitities.Base;

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
            return _dbContext.SportEvents.Where(a => a.Users.Contains(user)).ToList();
        }

        public SportEvent Save(SportEvent sportEvent)
        {
            _dbContext.SportEvents.Add(sportEvent);
            _dbContext.SaveChanges();

            return sportEvent;
        }
    }
}

