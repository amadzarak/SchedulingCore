using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingCore.Extensions;

public static class RecurrenceRuleExtensions
{
    public static IEnumerable<DateTime> GenerateOccurrences(
        this RecurrenceRule rule, DateTime? until = null)
    {
        var endDate = until ?? rule.EndDate ?? rule.StartDate.AddYears(1);
        var occurrences = new List<DateTime>();
        var current = rule.StartDate;
        var count = 0;

        return occurrences;
    }

    public static bool OccursOn(this RecurrenceRule rule, DateTime date)
    {
        if (date < rule.StartDate ||
            (rule.EndDate.HasValue && date > rule.EndDate.Value) ||
            (rule.ExcludedDates != null && rule.ExcludedDates.Contains(date)))
        {
            return false;
        }

        return ShouldIncludeOccurrences(rule, date);
    }

    private static bool ShouldIncludeOccurrences(RecurrenceRule rule, DateTime date) 
    {
        return true;
    }

}
