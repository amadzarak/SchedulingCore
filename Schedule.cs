
namespace SchedulingCore;

public class Schedule
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // NAVIGATION PROPERTIES
    public ICollection<ScheduledActivity> Activities { get; set; }
    public ICollection<RecurringActivity> RecurringActivities { get; set; }
}


public abstract class ScheduleManagement
{
    public abstract void AddActivity(ScheduledActivity activity);
    public abstract void RemoveActivity(Guid activityId);
    public abstract IEnumerable<ScheduledActivity> GetActivitiesInRange(DateTime start, DateTime end);
    public abstract IEnumerable<TimeRange> FindAvailableTimeSlots(DateTime start, DateTime end, TimeSpan duration);
    public abstract bool HasConflicts(ScheduledActivity activity);
}
