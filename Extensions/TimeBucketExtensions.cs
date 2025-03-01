using SchedulingCore.Entities;

namespace SchedulingCore.Extensions;

public static class TimeBucketExtensions
{
    public static bool CanFit(this TimeBucket timeBucket, TimeSpan duration) { return true; }
    public static void Assign(this TimeBucket timeBucket, ScheduledActivity activity) { }
    public static void UnAssign(this TimeBucket timeBucket, ScheduledActivity activity) { }
}
