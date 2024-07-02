using System;
using FitnessApp.Domain.Entities;
using FitnessApp.Domain.Entities.Base;

namespace FitnessApp.Domain.Helpers
{
    public static class SensorDataHelper
    {
        private static readonly Random Random = new Random();

        public static SensorData GenerateSensorData(SportActivity sportActivity)
        {
            var hours = (sportActivity.EndTime - sportActivity.StartTime).TotalHours;

            return new SensorData
            {
                Id = Guid.NewGuid(),
                AverageHeartRate = Random.NextDouble() * (160 - 90) + 90,
                CalorieConsumption = CalculateCalorieConsumption(hours),
                AverageBodyTemperature = Random.NextDouble() * (39 - 36) + 36,
                SportActivity = sportActivity,
                SportActivityId = sportActivity.Id
            };
        }

        private static double CalculateCalorieConsumption(double hours)
        {
            return hours * (Random.NextDouble() * (600 - 400) + 400);
        }
    }
}




