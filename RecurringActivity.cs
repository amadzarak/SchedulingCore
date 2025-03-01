
namespace SchedulingCore;

public class RecurringActivity
{
    public Guid Id { get; set; }
    public ScheduledActivity BaseActivity { get; set; }
    public Guid BaseActivityId { get; set; }
    public RecurrenceRule RecurrenceRule { get; set; }
    public Guid RecurrenceRuleId { get; set; }
    public ICollection<ScheduledActivity> Exceptions { get; set; }
}

public abstract class RecurringActivityManager
{
    public abstract IEnumerable<DateTime> GenerateOccurences(DateTime? until = null);
}
