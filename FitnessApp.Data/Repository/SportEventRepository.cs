using System.Collections.Generic;
using FitnessApp.Data.DBContext;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entities;
using FitnessApp.Domain.Entitities.Base;

namespace FitnessApp.Data.Repository
{
    public class SportEventRepository : ISportEventRepository
    {
        private readonly FitnessAppContext _dbContext;

        public SportEventRepository(FitnessAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SportEvent GetSportEventById(int eventId)
        {
            return _dbContext.SportEvents.Find(eventId);
        }

        public IEnumerable<SportEvent> GetAllSportEvents()
        {
            return _dbContext.SportEvents;
        }

        public void AddSportEvent(SportEvent sportEvent)
        {
            _dbContext.SportEvents.Add(sportEvent);
        }

        public void Save(SportEvent sportEvent)
        {
            _dbContext.SaveChanges();
        }
    }
}

