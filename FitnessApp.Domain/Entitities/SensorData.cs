using FitnessApp.Domain.Entities.Base;
using System;

namespace FitnessApp.Domain.Entities
{
    public class SensorData
    {
        public Guid Id { get; set; }
        public double AverageHeartRate { get; set; }
        public double CalorieConsumption { get; set; }
        public double AverageBodyTemperature { get; set; }
        public Guid SportActivityId { get; set; }
        public SportActivity SportActivity { get; set; }
    }
}



