using FitnessApp.Domain.Entities.Base;

namespace FitnessApp.Domain.Entitities;

public class BikeActivity : SportActivity
{
    public override string DistanceUnit => "Km";
}