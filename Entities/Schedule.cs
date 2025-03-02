namespace SchedulingCore.Entities;

public class Schedule
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // NAVIGATION PROPERTIES
    public ICollection<ScheduledActivity> Activities { get; set; } = [];
    public ICollection<RecurringActivity> RecurringActivities { get; set; } = [];
}
