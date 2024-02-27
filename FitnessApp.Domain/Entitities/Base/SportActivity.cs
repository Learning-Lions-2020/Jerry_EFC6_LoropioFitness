using FitnessApp.Domain.CustomTypes;
using FitnessApp.Domain.Entitities;

namespace FitnessApp.Domain.Entities.Base;

public class SportActivity
{
    public Guid Id { get; set; }
    public double Distance { get; set; }
    public TimeSpan TimeTaken { get; set; }
    public DateTime ActivityDate { get; set; }
    public Feeling Feeling { get; set; }
    public virtual string DistanceUnit => "";
    public User User { get; set; }
    public ActivityType ActivityType { get; set; }

    // Add UserId property
    public int UserId { get; set; }
}