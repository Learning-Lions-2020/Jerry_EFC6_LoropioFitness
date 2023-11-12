using FitnessApp.Domain.Entities.Base;

namespace FitnessApp.Domain.Entitities;

public class RunActivity : SportActivity
{
    public override string DistanceUnit => "Km";
}