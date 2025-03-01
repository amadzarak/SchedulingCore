namespace SchedulingCore;

public abstract class TimeRange
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TimeSpan Duration => End - Start;
}

public abstract class TimeRangeManager
{
    public abstract bool Overlaps(TimeRange other);
    public abstract bool Contains(TimeRange other);
    public abstract bool IsAdjacentTo(TimeRange other, TimeSpan tolerance = default);
    public abstract TimeRange MergeWith(TimeRange other);
}

public class FixedTimeRange : TimeRange
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public FixedTimeRange() { }

    public FixedTimeRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

}
public class FixedTimeRangeManager : TimeRangeManager
{
    public override bool Contains(TimeRange other)
    {
        throw new NotImplementedException();
    }
    public bool Contains(DateTime point)
    {
        throw new NotImplementedException();
//        return point >= Start && point < End;
    }
    public override bool IsAdjacentTo(TimeRange other, TimeSpan tolerance = default)
    {
        throw new NotImplementedException();
    }

    public override TimeRange MergeWith(TimeRange other)
    {
        throw new NotImplementedException();
    }

    public override bool Overlaps(TimeRange other)
    {
        throw new NotImplementedException();
//        return Start < other.End && End > other.Start;
    }
}
