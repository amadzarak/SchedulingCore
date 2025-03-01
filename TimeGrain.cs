
using System.Runtime.CompilerServices;

namespace SchedulingCore;

public class TimeGrain
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public TimeSpan Granularity { get; set; }
    public TimeGrain Next { get; set; }
    public TimeGrain Previous { get; set; }  
    
}

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
