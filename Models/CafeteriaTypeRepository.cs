using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class CafeteriaTypeRepository : CafeteriaTypeIRepository
    {
        private readonly AppDbContext _context;

        public CafeteriaTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CafeteriaType[]> GetCafeteriaTypeAsync()
        {
            IQueryable<CafeteriaType> query = _context.CafeteriaType;
            return await query.ToArrayAsync();
        }

        public async Task<CafeteriaType> GetCafeteriaType(Guid Id)
        {
            IQueryable<CafeteriaType> query = _context.CafeteriaType;
            return await query.FirstOrDefaultAsync(x => x.CafeteriaTypeId == Id);
        }

        public async Task<CafeteriaType> AddCafeteriaType(CafeteriaType newCafeType)
        {
            _context.CafeteriaType.Add(newCafeType);
            await _context.SaveChangesAsync();
            return newCafeType;
        }

        public async Task<CafeteriaType> EditCafeteriaType(CafeteriaType editedCafeType)
        {
            _context.Entry(editedCafeType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedCafeType;
        }

        public bool DeleteCafeteriaType(Guid Id)
        {


            //bool result = false;
            var cafeType = _context.CafeteriaType.Find(Id);
            if (cafeType == null)
            {
                //_context.Entry(sponsorType).State = EntityState.Deleted;
                //_context.SaveChanges();
                return false;
            }

            // Check if there are any cafeterias referencing this cafeteriaType
            bool hasReferencedCafes = _context.Cafeteria.Any(s => s.CafeteriaTypeId == Id);

            if (hasReferencedCafes)
            {
                // There are referenced cafeterias, so we cannot delete the cafeteriaType
                return false;
            }

            try
            {
                _context.CafeteriaType.Remove(cafeType);
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
