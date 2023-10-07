namespace Architecture.Models
{
    public class AttendanceType
    {
        public Guid AttendanceTypeId { get; internal set; } = Guid.NewGuid();
        public string AttendanceTypeDescription { get; set; }

        //public List<Attendance> Attendances { get; set; }
    }
}
