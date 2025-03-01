using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingCore.Services;

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
}
