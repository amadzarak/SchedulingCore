using SchedulingCore.Entities;
using SchedulingCore.Interfaces;

namespace SchedulingCore.Services;

public class ScheduleService : IScheduleService
{
    public void AddActivity(Schedule schedule, ScheduledActivity activity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TimeRange> FindAvailableTimeSlots(Schedule schedule, DateTime start, DateTime end, TimeSpan duration)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ScheduledActivity> GetActivitiesInRange(Schedule schedule, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TimeSlot> GetAllOccupiedTimeSlots(Schedule schedule)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TimeSlot> GetOccupiedTimeSlots(Schedule schedule, DateTime data)
    {
        throw new NotImplementedException();
    }

    public bool HasConflicts(Schedule schedule, ScheduledActivity activity)
    {
        throw new NotImplementedException();
    }

    public void RemoveActivity(Schedule schedule, Guid activityId)
    {
        throw new NotImplementedException();
    }
}
