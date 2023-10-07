namespace Architecture.Models
{
    public class Schedule
    {
        public Guid ScheduleId { get; internal set; } = Guid.NewGuid();
        public Guid ProgramId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
