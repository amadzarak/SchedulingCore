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
        var searchRange = new FixedTimeRange(start, end);
        
        // Get all activities that overlap with our search range
        var activitiesInRange = schedule.GetActivitiesInRange(start, end)
            .Select(a => a.TimeRange)
            .OrderBy(tr => tr.Start)
            .ToList();
        
        // If no activities, the entire range is available
        if (!activitiesInRange.Any())
        {
            if (end - start >= duration)
            {
                availableSlots.Add(new FixedTimeRange(start, end));
            }
            return availableSlots;
        }
        
        // Check for available slot before the first activity
        var firstActivityStart = activitiesInRange.First().Start;
        if (firstActivityStart > start && (firstActivityStart - start) >= duration)
        {
            availableSlots.Add(new FixedTimeRange(start, firstActivityStart));
        }
        
        // Check for available slots between activities
        for (int i = 0; i < activitiesInRange.Count - 1; i++)
        {
            var currentEnd = activitiesInRange[i].End;
            var nextStart = activitiesInRange[i + 1].Start;
            
            if (nextStart > currentEnd && (nextStart - currentEnd) >= duration)
            {
                availableSlots.Add(new FixedTimeRange(currentEnd, nextStart));
            }
        }
        
        // Check for available slot after the last activity
        var lastActivityEnd = activitiesInRange.Last().End;
        if (end > lastActivityEnd && (end - lastActivityEnd) >= duration)
        {
            availableSlots.Add(new FixedTimeRange(lastActivityEnd, end));
        }

        return availableSlots;

    }

    public static IEnumerable<TimeRange> GetOccupiedTimeSlots(this Schedule schedule, DateTime date)
    {
        var dayStart = date.Date;
        var dayEnd = dayStart.AddDays(1).AddTicks(-1);

        var activitiesOnDay = schedule.GetActivitiesInRange(dayStart, dayEnd);
        return activitiesOnDay.Select(a => a.TimeRange).ToList();

    }
}
