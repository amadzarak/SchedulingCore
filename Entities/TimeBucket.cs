﻿namespace SchedulingCore.Entities;

public class TimeBucket
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TimeSpan Capacity { get; set; }
    public TimeSpan UsedCapacity { get; set; }
    public TimeSpan RemainingCapacity => Capacity - UsedCapacity;
    // NAVIGATION PROPERTIES
    public virtual ICollection<ScheduledActivity> Activities { get; set; }
}
