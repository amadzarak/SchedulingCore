using SchedulingCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingCore.Interfaces;

public interface IRecurrenceService
{
    Task<IEnumerable<DateTime>> GenerateOccurrencesAsync(
        Guid recurringActivityId,
        DateTime start,
        DateTime end);

    Task AddExceptionsAsync(Guid recurringActivityId, ScheduledActivity exception);
    Task RemoveExceptionsAsync(Guid recurringActivityId, Guid exceptionId);

    Task<IEnumerable<ScheduledActivity>> GetCachedOccurrencesAsync(
        Guid recurringActivityId,
        DateTime start,
        DateTime end);
}
