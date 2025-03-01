
using SchedulingCore.Shared;

namespace SchedulingCore;

public class RecurrenceRule
{
    public Guid Id { get; set; }
    public RecurrenceFrequency Frequency { get; set; }
    public int Interval { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? Count { get; set; }

    // FOR WEEKLY RECURRENCES
    public DayOfWeek[] DaysOfWeek { get; set; }

    // FOR MONTHLY RECURRENCES
    public int[] DaysOfMonth { get; set; }
    public MonthlyRecurrenceType MonthlyType { get; set; }
    public int? WeekOfMonth { get; set; }

    // FOR EXCLUSIONS
    public DateTime[] ExcludedDates { get; set; }
}
