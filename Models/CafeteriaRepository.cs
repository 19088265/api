using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class CafeteriaRepository : CafeteriaIRepository
    {
        private readonly AppDbContext _context;

        public CafeteriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cafeteria[]> GetCafeteriaAsync()
        {
            IQueryable<Cafeteria> query = _context.Cafeteria.Include(e => e.CafeteriaType);
            return await query.ToArrayAsync();
        }

        public async Task<Cafeteria> GetCafeteria(Guid Id)
        {
            IQueryable<Cafeteria> query = _context.Cafeteria.Where(c => c.CafeteriaId == Id).Include(e => e.CafeteriaType);
            return await query.FirstOrDefaultAsync(x => x.CafeteriaId == Id);
        }

        //Add Cafeteria
        public async Task<Cafeteria> AddCafeteria(Cafeteria newCafeteria)
        {
            _context.Cafeteria.Add(newCafeteria);
            await _context.SaveChangesAsync();
            return newCafeteria;
        }

        //Update Cafeteria
        public async Task<Cafeteria> EditCafeteria(Cafeteria editedCafe)
        {
            _context.Entry(editedCafe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedCafe;
        }

        //Delete Cafeteria

        public bool DeleteCafeteria(Guid Id)
        {
            //bool result = false;
            var cafe = _context.Cafeteria.Find(Id);
            if (cafe == null)
            {
                // _context.Entry(sponsor).State = EntityState.Deleted;
                // _context.SaveChanges();
                return false;
            }
            bool hasReferencedAttendances = _context.Attendance.Any(s => s.CafeteriaId == Id);

            if (hasReferencedAttendances)
            {
                // There are referenced attendances, so we cannot delete the cafeteria
                return false;
            }

            try
            {
                _context.Cafeteria.Remove(cafe);
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
