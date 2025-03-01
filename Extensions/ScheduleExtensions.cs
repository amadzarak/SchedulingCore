using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingCore.Extensions;

public static class ScheduleExtensions
{
    public static void AddActivity(this Schedule schedule, ScheduledActivity activity)
    {
        activity.ScheduleId = schedule.Id;
        activity.Schedule = schedule;

        schedule.Activities.Add(activity);
    }

    public static bool HasConflicts(this Schedule schedule, ScheduledActivity activity)
    {
        // Might need to revisit this.
        // Basically the a.Id != activity.Id check is because if the ids are the same
        // we are dealing with the same activity. As such they would obviously overlap.
        // So we do this check and if overlap is still true then we are for sure certain we
        // have a conflict.

        return schedule.Activities.Any(a => a.Id != activity.Id && a.TimeRange.Overlaps(activity.TimeRange));
    }

    public static IEnumerable<ScheduledActivity> GetActivitiesInRange(this Schedule schedule,
        DateTime start,
        DateTime end)
    {
        var timeRange = new FixedTimeRange(start, end);
        // Certainly could also use the Contains method whereby we would do timeRange.Contains(a.TimeRange)
        // Will do some benchmarking to see which is better
        return schedule.Activities.Where(a => a.TimeRange.Overlaps(timeRange));
    }

    public static IEnumerable<TimeRange> FindAvailableTimeSlots(
        this Schedule schedule, DateTime start, DateTime end, TimeSpan duration)
    {
        var availableSlots = new List<TimeRange>();

        return availableSlots;

    }
}
