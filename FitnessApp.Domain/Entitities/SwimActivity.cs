using FitnessApp.Domain.Entities.Base;

namespace FitnessApp.Domain.Entitities
{
    public class SwimActivity : SportActivity
    {
        public override string DistanceUnit => "M";
    }
}
