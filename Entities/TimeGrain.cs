using System.Runtime.CompilerServices;

namespace SchedulingCore.Entities;

public class TimeGrain
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public TimeSpan Granularity { get; set; }
    public TimeGrain Next { get; set; }
    public TimeGrain Previous { get; set; }
}
