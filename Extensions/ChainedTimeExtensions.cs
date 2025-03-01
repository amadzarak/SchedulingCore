using SchedulingCore.Entities;

namespace SchedulingCore.Extensions;

public static class ChainedTimeExtensions
{
    public static IEnumerable<ScheduledActivity> ChainActivities(
        this IEnumerable<ScheduledActivity> activities,
        DateTime startTime,
        bool includeDeterministicGaps = false,
        TimeSpan? gapDuration = null)
    {
        if (activities == null)
        {
            throw new ArgumentNullException(nameof(activities));
        }
        
        var activityList = activities.ToList();
        if (!activityList.Any())
        {
            return activityList;
        }
        
        // Start with the first activity
        var currentTime = startTime;
        
        foreach (var activity in activityList)
        {
            // If no time range exists, create one with a default duration
            var duration = activity.TimeRange?.Duration ?? TimeSpan.FromHours(1);
            
            // Set the activity's time range
            activity.TimeRange = new FixedTimeRange(currentTime, currentTime.Add(duration));
            
            // Calculate next start time
            currentTime = activity.TimeRange.End;
            
            // Add gap if needed
            if (includeDeterministicGaps && gapDuration.HasValue)
            {
                currentTime = currentTime.Add(gapDuration.Value);
            }
        }
        
        return activityList;
    }
    public static IEnumerable<ScheduledActivity> AppendToChain(
        this IEnumerable<ScheduledActivity> existingChain,
        ScheduledActivity newActivity,
        bool includeGap = false,
        TimeSpan? gapDuration = null)
    {
        if (existingChain == null)
        {
            throw new ArgumentNullException(nameof(existingChain));
        }
        
        if (newActivity == null)
        {
            throw new ArgumentNullException(nameof(newActivity));
        }
        
        var chain = existingChain.ToList();
        
        // If chain is empty, return just the new activity with its existing time range
        if (!chain.Any() || chain.All(a => a.TimeRange == null))
        {
            return new[] { newActivity };
        }
        
        // Find the last activity in the chain
        var lastActivity = chain
            .Where(a => a.TimeRange != null)
            .OrderBy(a => a.TimeRange.End)
            .LastOrDefault();
        
        if (lastActivity == null)
        {
            return chain.Append(newActivity);
        }
        
        // Calculate start time for the new activity
        var startTime = lastActivity.TimeRange.End;
        
        // Add gap if needed
        if (includeGap && gapDuration.HasValue)
        {
            startTime = startTime.Add(gapDuration.Value);
        }
        
        // Set the time range for the new activity
        var duration = newActivity.TimeRange?.Duration ?? TimeSpan.FromHours(1);
        newActivity.TimeRange = new FixedTimeRange(startTime, startTime.Add(duration));
        
        // Add to the chain and return
        return chain.Append(newActivity);
    }
    public static IEnumerable<ScheduledActivity> InsertIntoChain(
        this IEnumerable<ScheduledActivity> existingChain,
        ScheduledActivity newActivity,
        ScheduledActivity insertAfterActivity = null,
        bool includeGaps = false,
        TimeSpan? gapDuration = null)
    {
        if (existingChain == null)
        {
            throw new ArgumentNullException(nameof(existingChain));
        }
        
        if (newActivity == null)
        {
            throw new ArgumentNullException(nameof(newActivity));
        }
        
        var chain = existingChain.ToList();
        
        // If chain is empty, return just the new activity
        if (!chain.Any())
        {
            return new[] { newActivity };
        }
        
        // If inserting at the beginning
        if (insertAfterActivity == null)
        {
            // Find the first activity
            var firstActivity = chain
                .Where(a => a.TimeRange != null)
                .OrderBy(a => a.TimeRange.Start)
                .FirstOrDefault();
            
            if (firstActivity == null)
            {
                return chain.Prepend(newActivity);
            }
            
            // Set time range for new activity, starting at the same time as the first activity
            var startTime = firstActivity.TimeRange.Start;
            var duration = newActivity.TimeRange?.Duration ?? TimeSpan.FromHours(1);
            newActivity.TimeRange = new FixedTimeRange(startTime, startTime.Add(duration));
            
            // Add the new activity to the beginning
            var result = new List<ScheduledActivity> { newActivity };
            
            // Adjust all other activities
            var currentTime = newActivity.TimeRange.End;
            if (includeGaps && gapDuration.HasValue)
            {
                currentTime = currentTime.Add(gapDuration.Value);
            }
            
            foreach (var activity in chain.OrderBy(a => a.TimeRange?.Start))
            {
                if (activity.TimeRange == null)
                {
                    result.Add(activity);
                    continue;
                }
                
                var activityDuration = activity.TimeRange.Duration;
                activity.TimeRange = new FixedTimeRange(currentTime, currentTime.Add(activityDuration));
                
                result.Add(activity);
                
                currentTime = activity.TimeRange.End;
                if (includeGaps && gapDuration.HasValue)
                {
                    currentTime = currentTime.Add(gapDuration.Value);
                }
            }
            
            return result;
        }
        else
        {
            // Insert after a specific activity
            var insertIndex = chain.IndexOf(insertAfterActivity);
            if (insertIndex < 0)
            {
                throw new ArgumentException("Insert after activity not found in chain");
            }
            
            // Set time range for new activity
            var startTime = insertAfterActivity.TimeRange?.End ?? DateTime.Now;
            if (includeGaps && gapDuration.HasValue)
            {
                startTime = startTime.Add(gapDuration.Value);
            }
            
            var duration = newActivity.TimeRange?.Duration ?? TimeSpan.FromHours(1);
            newActivity.TimeRange = new FixedTimeRange(startTime, startTime.Add(duration));
            
            // Create the result chain
            var result = new List<ScheduledActivity>();
            
            // Add all activities up to and including the insertAfterActivity
            for (int i = 0; i <= insertIndex; i++)
            {
                result.Add(chain[i]);
            }
            
            // Add the new activity
            result.Add(newActivity);
            
            // Add remaining activities with adjusted times
            var currentTime = newActivity.TimeRange.End;
            if (includeGaps && gapDuration.HasValue)
            {
                currentTime = currentTime.Add(gapDuration.Value);
            }
            
            for (int i = insertIndex + 1; i < chain.Count; i++)
            {
                var activity = chain[i];
                
                if (activity.TimeRange == null)
                {
                    result.Add(activity);
                    continue;
                }
                
                var activityDuration = activity.TimeRange.Duration;
                activity.TimeRange = new FixedTimeRange(currentTime, currentTime.Add(activityDuration));
                
                result.Add(activity);
                
                currentTime = activity.TimeRange.End;
                if (includeGaps && gapDuration.HasValue)
                {
                    currentTime = currentTime.Add(gapDuration.Value);
                }
            }
            
            return result;
        }
    }
    public static TimeSpan GetChainDuration(
        this IEnumerable<ScheduledActivity> chain,
        bool includeGaps = false,
        TimeSpan? gapDuration = null)
    {
        if (chain == null)
        {
            throw new ArgumentNullException(nameof(chain));
        }

        var activities = chain
            .Where(a => a.TimeRange != null)
            .OrderBy(a => a.TimeRange.Start)
            .ToList();

        if (!activities.Any())
        {
            return TimeSpan.Zero;
        }

        // If we're not including gaps, simply sum the durations
        if (!includeGaps || !gapDuration.HasValue)
        {
            return activities.Aggregate(TimeSpan.Zero, (sum, a) => sum.Add(a.TimeRange.Duration));
        }

        // Otherwise, calculate from start to end
        var firstActivity = activities.First();
        var lastActivity = activities.Last();

        return lastActivity.TimeRange.End - firstActivity.TimeRange.Start;
    }
    public static IEnumerable<ScheduledActivity> AddBreaksAfter(
        this IEnumerable<ScheduledActivity> chain,
        Func<ScheduledActivity, bool> breakCondition,
        TimeSpan breakDuration)
    {
        if (chain == null)
        {
            throw new ArgumentNullException(nameof(chain));
        }
        
        if (breakCondition == null)
        {
            throw new ArgumentNullException(nameof(breakCondition));
        }
        
        var activities = chain
            .Where(a => a.TimeRange != null)
            .OrderBy(a => a.TimeRange.Start)
            .ToList();
        
        if (!activities.Any())
        {
            return activities;
        }
        
        var result = new List<ScheduledActivity>();
        var currentTime = activities.First().TimeRange.Start;
        
        foreach (var activity in activities)
        {
            // Set the activity's new start time
            var duration = activity.TimeRange.Duration;
            activity.TimeRange = new FixedTimeRange(currentTime, currentTime.Add(duration));
            result.Add(activity);
            
            // Move current time to end of this activity
            currentTime = activity.TimeRange.End;
            
            // Add break if needed
            if (breakCondition(activity))
            {
                currentTime = currentTime.Add(breakDuration);
            }
        }
        
        return result;
    }
}
