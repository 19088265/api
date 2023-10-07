namespace Architecture.Models
{
    public interface ScheduleIRepository
    {
        Task<Schedule[]> GetScheduleAsync();

        Task<Schedule> GetSchedule(Guid ScheduleId);
        Task<Schedule> AddSchedule(Schedule newSchedule);
        Task<Schedule> EditSchedule(Schedule editedSchedule);
        bool DeleteSchedule(Guid id);
    }
}
