using FitnessApp.Data.DBContext;
using FitnessApp.Domain.Contracts;
using FitnessApp.Domain.Entitities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Data.Repository
{
    public class RawSQLSportEventRepository : ISportEventRepository
    {
        private FitnessAppContext _dbContext;

        public RawSQLSportEventRepository(FitnessAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SportEvent GetSportEventById(int eventId)
        {
            return _dbContext.SportEvents
                .FromSqlInterpolated($"SELECT * FROM SportEvents WHERE Id = {eventId}")
                .FirstOrDefault();
        }

        public List<SportEvent> GetAllSportEvents()
        {
            return _dbContext.SportEvents
                .FromSqlRaw("SELECT * FROM SportEvents")
                .ToList();
        }

        public List<SportEvent> GetMySportEvents(User user)
        {
            return _dbContext.SportEvents
                .FromSqlInterpolated($"SELECT * FROM SportEvents WHERE Id IN (SELECT SportEventId FROM UserSportEvents WHERE UserId = {user.Id})")
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
