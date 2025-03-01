using SchedulingCore.Entities;
using SchedulingCore.Extensions;
using SchedulingCore.Interfaces;
namespace SchedulingCore.Services;

public class RecurrenceService : IRecurrenceService
{
    private readonly IRepository<RecurrenceRule> recurrenceRepository;
    private readonly IRepository<Schedule> scheduleRepository;

    public RecurrenceService(IRepository<RecurrenceRule> _recurrenceRepository, IRepository<Schedule> _scheduleRepository)
    {
        recurrenceRepository = _recurrenceRepository;
        scheduleRepository = _scheduleRepository;
    }


    public async Task AddExceptionsAsync(Guid recurringActivityId, ScheduledActivity exception)
    {
        // Get rrule from database
        RecurrenceRule rrule = await recurrenceRepository.GetByIdAsync(recurringActivityId);
        if (rrule == null) { return; }

        // add scheduled activity to Schedule exceptions table.
        // TODO: After implementationg of persistence layer.
    }

    public async Task<IEnumerable<DateTime>> GenerateOccurrencesAsync(Guid recurringActivityId, DateTime start, DateTime end)
    {
        RecurrenceRule rrule = await recurrenceRepository.GetByIdAsync(recurringActivityId);
        if (rrule == null) { return []; }

        return rrule.GenerateOccurrences();
    }

    public Task<IEnumerable<ScheduledActivity>> GetCachedOccurrencesAsync(Guid recurringActivityId, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public Task RemoveExceptionsAsync(Guid recurringActivityId, Guid exceptionId)
    {
        // I would most likely remove using schedule repository
        throw new NotImplementedException();
    }
}
