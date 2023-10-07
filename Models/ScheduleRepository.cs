using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class ScheduleRepository : ScheduleIRepository
    {
        private readonly AppDbContext _context;

        public ScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Schedule[]> GetScheduleAsync()
        {
            IQueryable<Schedule> query = _context.Schedule;
            return await query.ToArrayAsync();
        }

        public async Task<Schedule> GetSchedule(Guid Id)
        {
            IQueryable<Schedule> query = _context.Schedule;
            return await query.FirstOrDefaultAsync(x => x.ScheduleId == Id);
        }

        public async Task<Schedule> AddSchedule(Schedule newSchedule)
        {
            _context.Schedule.Add(newSchedule);
            await _context.SaveChangesAsync();
            return newSchedule;
        }

        public async Task<Schedule> EditSchedule(Schedule editedSchedule)
        {
            _context.Entry(editedSchedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedSchedule;
        }

        public bool DeleteSchedule(Guid Id)
        {
            bool result = false;
            var sched = _context.Schedule.Find(Id);
            if (sched != null)
            {
                _context.Entry(sched).State = EntityState.Deleted;
                _context.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

    }
}
