using System;

namespace FitnessApp.Domain.Entitities.Base
{
    public class SportEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
