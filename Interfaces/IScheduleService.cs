using SchedulingCore.Entities;

namespace SchedulingCore.Interfaces;
public interface IScheduleService
{
    void AddActivity(Schedule schedule, ScheduledActivity activity);
    void RemoveActivity(Schedule schedule, Guid activityId);

    IEnumerable<ScheduledActivity> GetActivitiesInRange(Schedule schedule, DateTime start, DateTime end);
    IEnumerable<TimeRange> FindAvailableTimeSlots(Schedule schedule,
        DateTime start,
        DateTime end,
        TimeSpan duration);

    bool HasConflicts(Schedule schedule, ScheduledActivity activity);

    IEnumerable<TimeSlot> GetOccupiedTimeSlots(Schedule schedule, DateTime data);
    IEnumerable<TimeSlot> GetAllOccupiedTimeSlots(Schedule schedule);
}
