using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class AttendanceTypeRepository : AttendanceTypeIRepository
    {
        private readonly AppDbContext _context;

        public AttendanceTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AttendanceType> GetAttendanceType(Guid Id)
        {
            IQueryable<AttendanceType> query = _context.AttendanceType;
            return await query.FirstOrDefaultAsync(x => x.AttendanceTypeId == Id);
        }
    }
}
