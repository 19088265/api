namespace Architecture.Models
{
    public interface AttendanceTypeIRepository
    {
        Task<AttendanceType> GetAttendanceType(Guid AttendanceTypeId);
    }
}
