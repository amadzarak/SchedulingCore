using System.Runtime.ExceptionServices;

namespace SchedulingCore.Extensions;

public static class TimeRangeExtensions
{
    public static bool Overlaps(this TimeRange lhs, TimeRange other)
    {
        return lhs.Start < other.End && lhs.End > other.Start;
    }

    public static bool Contains(this TimeRange container, TimeRange contained)
    {
        return container.Start <= contained.Start && container.End >= contained.End;
    }

    public static bool Contains(this TimeRange range, DateTime instant)
    {
        return instant >= range.Start && instant <= range.End;
    }

    public static bool IsAdjacentTo(this TimeRange lhs, TimeRange rhs, TimeSpan tolerance = default)
    {
        var gap = (lhs.End < rhs.Start) ? rhs.Start - lhs.End : lhs.Start - rhs.End;
        return gap >= TimeSpan.Zero && gap < tolerance;
    }

    public static TimeRange MergeWith(this TimeRange lhs, TimeRange rhs)
    {
        return new FixedTimeRange()
        {
            Start = lhs.Start < rhs.Start ? lhs.Start : rhs.Start,
            End = lhs.End > rhs.End ? lhs.End : rhs.End
        };
    }
}
