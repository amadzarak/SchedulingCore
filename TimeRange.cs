namespace SchedulingCore;

public abstract class TimeRange
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TimeSpan Duration => End - Start;
}

public class FixedTimeRange : TimeRange
{
    public FixedTimeRange() { }

    public FixedTimeRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }
}
