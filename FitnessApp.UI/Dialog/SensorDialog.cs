using FitnessApp.Data.DBContext;
using FitnessApp.Domain.CustomTypes;
using FitnessApp.Domain.Entities;
using System;
using System.Linq;

namespace FitnessApp.UI.Dialog
{
    public class SensorDialog
    {
        private readonly FitnessAppContext _dbContext;

        public SensorDialog(FitnessAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ViewAllSensorData()
        {
            var sensorDataList = _dbContext.SensorDatas.ToList();

            if (sensorDataList.Any())
            {
                foreach (var sensorData in sensorDataList)
                {
                    Console.WriteLine($"Sensor Data ID: {sensorData.Id}");
                    Console.WriteLine($"Average Heart Rate: {sensorData.AverageHeartRate}");
                    Console.WriteLine($"Average Body Temperature: {sensorData.AverageBodyTemperature}");
                    Console.WriteLine($"Calorie Consumption: {sensorData.CalorieConsumption}");
                    Console.WriteLine($"Activity Type: {sensorData.SportActivity.ActivityType}");
                    Console.WriteLine($"Activity Date: {sensorData.SportActivity.ActivityDate}");
                    Console.WriteLine("-------------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No sensor data found.");
            }
        }


        public void ViewSensorDataByActivityType()
        {
            Console.WriteLine("Select an activity type:");
            Console.WriteLine("1. BikeActivity");
            Console.WriteLine("2. ClimbActivity");
            Console.WriteLine("3. RunActivity");
            Console.WriteLine("4. SwimActivity");

            var input = Console.ReadLine();

            if (int.TryParse(input, out int activityTypeNumber) && activityTypeNumber >= 1 && activityTypeNumber <= 4)
            {
                ActivityType selectedActivityType = (ActivityType)(activityTypeNumber - 1);
                var sensorDataList = _dbContext.SensorDatas
                    .Where(sd => sd.SportActivity.ActivityType == selectedActivityType)
                    .ToList();

                if (sensorDataList.Any())
                {
                    foreach (var sensorData in sensorDataList)
                    {
                        Console.WriteLine($"Sensor Data ID: {sensorData.Id}");
                        Console.WriteLine($"Average Heart Rate: {sensorData.AverageHeartRate}");
                        Console.WriteLine($"Average Body Temperature: {sensorData.AverageBodyTemperature}");
                        Console.WriteLine($"Calorie Consumption: {sensorData.CalorieConsumption}");
                        Console.WriteLine($"Activity Date: {sensorData.SportActivity.ActivityDate}");
                        Console.WriteLine("-------------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine($"No sensor data found for activity type {selectedActivityType}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
            }
        }
    }
}

