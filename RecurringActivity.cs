
namespace SchedulingCore;

public class RecurringActivity
{
    public Guid Id { get; set; }
    public required ScheduledActivity BaseActivity { get; set; }
    public Guid BaseActivityId { get; set; }
    public required RecurrenceRule RecurrenceRule { get; set; }
    public Guid RecurrenceRuleId { get; set; }
    public ICollection<ScheduledActivity> Exceptions { get; set; } = [];
}
