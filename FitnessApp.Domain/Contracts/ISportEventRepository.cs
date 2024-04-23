using FitnessApp.Domain.Entitities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Domain.Contracts
{
    public interface ISportEventRepository
    {
        void AddSportEvent(SportEvent sportEvent);
        void Save(SportEvent sportEvent); // Add this method
    }
}
