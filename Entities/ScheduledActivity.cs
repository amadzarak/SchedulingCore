using SchedulingCore.Shared;

namespace SchedulingCore.Entities;

public class ScheduledActivity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ActivityStatus Status { get; set; }
    public TimeRange TimeRange { get; set; }
    public string ActivityType { get; set; }


    // OPTIONAL: TimeGrain reference for grain-based scheduling
    public int? StartingTimeGrainId { get; set; }

    // OPTIONAL: Metadata
    public Dictionary<string, string> Metadata { get; set; }
    // NAVIGATION PROPERTIES
    public virtual Schedule Schedule { get; set; }
    public Guid ScheduleId { get; set; }

}
