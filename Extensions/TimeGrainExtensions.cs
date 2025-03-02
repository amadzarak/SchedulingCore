using SchedulingCore.Entities;

namespace SchedulingCore.Extensions;

public static class TimeGrainExtensions
{
    /// <summary>
    /// Converts a TimeGrain object into a FixedTimeRange
    /// </summary>
    /// <param name="grain"></param>
    /// <returns></returns>
    public static TimeRange ToTimeRange(this TimeGrain grain)
    {
        return new FixedTimeRange()
        {
            Start = grain.Start,
            End = grain.Start.Add(grain.Granularity)
        };
    }
}
