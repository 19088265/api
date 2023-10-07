using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class AttendanceRepository : AttendanceIRepository
    {
        private readonly AppDbContext _context;
        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Attendance[]> GetAttendanceAsync()
        {
            IQueryable<Attendance> query = _context.Attendance;
            return await query.ToArrayAsync();
        }

        public async Task<Attendance> GetAttendance(Guid Id)
        {
            IQueryable<Attendance> query = _context.Attendance;
            return await query.FirstOrDefaultAsync(x => x.AttendanceId == Id);
        }

        // public async Task<Beneficiary[]> GetBeneficiariesByCafeteriaId(Guid CafeteriaId)
        // {
        // Get the attendance records for the given cafeteria session ID
        // var attendanceRecords = await _context.Attendance
        //.Where(a => a.CafeteriaId == CafeteriaId)
        // .ToListAsync();

        // Extract the beneficiary IDs from the attendance records
        // var beneficiaryIds = attendanceRecords.Select(a => a.BeneficiaryId).ToList();

        // Fetch the beneficiaries associated with the extracted beneficiary IDs
        //var beneficiaries = await _context.Beneficiary
        //.Where(b => beneficiaryIds.Contains(b.BeneficiaryId))
        //.ToArrayAsync();

        //return beneficiaries;
        //}
        public async Task<Attendance> AddAttendance(Attendance newAttendance)
        {
            _context.Attendance.Add(newAttendance);
            await _context.SaveChangesAsync();
            return newAttendance;
        }
        public async Task<bool> SaveAttendance(Attendance[] attendanceData)
        {
            try
            {
                // Add the attendance data to the database
                _context.Attendance.AddRange(attendanceData);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Attendance> TakeAttendance(Attendance newAttend)
        {
            _context.Attendance.Add(newAttend);
            await _context.SaveChangesAsync();
            return newAttend;
        }

        //AttendanceType

        public async Task<AttendanceType[]> GetAttendanceTypeAsync()
        {
            IQueryable<AttendanceType> query = _context.AttendanceType;
            return await query.ToArrayAsync();
        }

        //Add AttendanceType
        public async Task<AttendanceType> AddAttendanceType(AttendanceType newType)
        {
            _context.AttendanceType.Add(newType);
            await _context.SaveChangesAsync();
            return newType;
        }

        //Edit Attendance Type
        public async Task<AttendanceType> EditAttendanceType(AttendanceType editedAttendanceType)
        {
            _context.Entry(editedAttendanceType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedAttendanceType;
        }


        //Delete AttendanceType
        public bool DeleteAttendanceType(Guid Id)
        {
            //bool result = false;
            var attendanceType = _context.AttendanceType.Find(Id);
            if (attendanceType == null)
            {
                // _context.Entry(sponsor).State = EntityState.Deleted;
                // _context.SaveChanges();
                return false;
            }
            bool hasReferencedAttendances = _context.Attendance.Any(s => s.AttendanceTypeId == Id);

            if (hasReferencedAttendances)
            {
                // There are referenced attendances, so we cannot delete the AttendanceType
                return false;
            }

            try
            {
                _context.AttendanceType.Remove(attendanceType);
                _context.SaveChanges();
                return true; // Deletion successful
            }
            catch (Exception)
            {
                // Handle any exception that may occur during deletion
                return false; // Deletion failed
            }

        }
    }
}
