namespace SchedulingCore.Shared;

public enum ActivityStatus
{
    Planned,
    InProgress,
    Completed,
    Cancelled
}

public enum RecurrenceFrequency
{
    Daily,
    Weekly,
    Monthly,
    Yearly
}

public enum MonthlyRecurrenceType
{
    ByDayOfMonth,  // e.g., 15th of each month
    ByDayOfWeek    // e.g., 2nd Tuesday of each month
}
