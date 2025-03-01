
using SchedulingCore.Entities;

namespace SchedulingCore.Extensions;

public static class TimeSlotExtensions
{
    public static TimeRange ToTimeRange(this TimeSlot slot, DateTime date)
    {
        if (slot == null)
        {
            throw new ArgumentNullException(nameof(slot));
        }

        var start = date.Date.Add(slot.StartTime);
        var end = start.Add(slot.Duration);
        
        return new FixedTimeRange(start, end);
    }
    public static bool IsApplicableOn(this TimeSlot slot, DateTime date)
    {
        if (slot == null)
        {
            throw new ArgumentNullException(nameof(slot));
        }
        
        // If no specific days are set, the slot is applicable on any day
        if (slot.ApplicableDays == null || slot.ApplicableDays.Length == 0)
        {
            return true;
        }
        
        return slot.ApplicableDays.Contains(date.DayOfWeek);
    }
    public static IEnumerable<TimeSlot> GenerateTimeSlots(
        TimeSpan startTime, 
        TimeSpan endTime, 
        TimeSpan slotDuration, 
        DayOfWeek[] applicableDays = null)
    {
        if (startTime >= endTime)
        {
            throw new ArgumentException("Start time must be before end time");
        }
        
        if (slotDuration <= TimeSpan.Zero)
        {
            throw new ArgumentException("Slot duration must be positive");
        }
        
        var slots = new List<TimeSlot>();
        var currentTime = startTime;
        var id = 0;
        
        while (currentTime + slotDuration <= endTime)
        {
            slots.Add(new TimeSlot
            {
                Id = id++,
                StartTime = currentTime,
                Duration = slotDuration,
                ApplicableDays = applicableDays
            });
            
            currentTime = currentTime.Add(slotDuration);
        }
        
        return slots;
    }
    public static bool AssignActivity(this TimeSlot slot, ScheduledActivity activity, DateTime date)
    {
        if (slot == null)
        {
            throw new ArgumentNullException(nameof(slot));
        }
        
        if (activity == null)
        {
            throw new ArgumentNullException(nameof(activity));
        }
        
        if (!slot.IsApplicableOn(date))
        {
            return false;
        }
        
        // Set the time range based on the slot
        activity.TimeRange = slot.ToTimeRange(date);
        
        return true;
    }
    public static IEnumerable<(TimeSlot Slot, DateTime Date)> FindAvailableSlots(
        this IEnumerable<TimeSlot> slots,
        Schedule schedule,
        DateTime startDate,
        DateTime endDate)
    {
        if (slots == null)
        {
            throw new ArgumentNullException(nameof(slots));
        }
        
        if (schedule == null)
        {
            throw new ArgumentNullException(nameof(schedule));
        }
        
        var currentDate = startDate.Date;
        
        while (currentDate <= endDate.Date)
        {
            // Get occupied time ranges for this date
            var occupiedRanges = schedule.GetOccupiedTimeSlots(currentDate);
            
            foreach (var slot in slots)
            {
                // Skip if slot is not applicable for this day
                if (!slot.IsApplicableOn(currentDate))
                {
                    continue;
                }
                
                // Convert slot to time range for this date
                var slotRange = slot.ToTimeRange(currentDate);
                
                // Check if this slot overlaps with any occupied time
                var isAvailable = !occupiedRanges.Any(r => r.Overlaps(slotRange));
                
                if (isAvailable)
                {
                    yield return (slot, currentDate);
                }
            }
            
            currentDate = currentDate.AddDays(1);
        }
    }
    public static IEnumerable<TimeSlot> GetSlotsForDate(this IEnumerable<TimeSlot> slots, DateTime date)
    {
        if (slots == null)
        {
            throw new ArgumentNullException(nameof(slots));
        }
        
        return slots.Where(s => s.IsApplicableOn(date));
    }
}
