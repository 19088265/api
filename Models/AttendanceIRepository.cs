namespace Architecture.Models
{
    public interface AttendanceIRepository
    {
        Task<Attendance[]> GetAttendanceAsync();
        Task<Attendance> GetAttendance(Guid AttendanceId);
        //Fetch beneficiaries via cafeteria
        //Task<Beneficiary[]> GetBeneficiariesByCafeteriaId(Guid CafeteriaId);
        //Add Attendance
        Task<Attendance> AddAttendance(Attendance attendance);
        //Save attendance
        Task<bool> SaveAttendance(Attendance[] attendanceData);
        Task<Attendance> TakeAttendance(Attendance newAttend);


        //AttendanceType
        Task<AttendanceType[]> GetAttendanceTypeAsync();
        //Task<AttendanceType> GetAttendanceType(Guid AttendanceTypeId);
        Task<AttendanceType> AddAttendanceType(AttendanceType newType);
        Task<AttendanceType> EditAttendanceType(AttendanceType editedAttendanceType);
        bool DeleteAttendanceType(Guid Id);
    }
}
