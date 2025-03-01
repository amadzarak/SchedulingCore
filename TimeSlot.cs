
namespace SchedulingCore;

public class TimeSlot
{
    public int Id { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public TimeSpan EndTime => StartTime + Duration;
    public DayOfWeek[] ApplicableDays { get; set; }

}
