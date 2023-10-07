using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class SuburbRepository : SuburbIRepository
    {
        private readonly AppDbContext _context;

        public SuburbRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Suburb[]> GetSuburbAsync()
        {
            IQueryable<Suburb> query = _context.Suburb;
            return await query.ToArrayAsync();
        }

        public async Task<Suburb> GetSuburb(Guid Id)
        {
            IQueryable<Suburb> query = _context.Suburb;
            return await query.FirstOrDefaultAsync(x => x.SuburbId == Id);
        }

        public async Task<Suburb> AddSuburb(Suburb newSuburb)
        {
            _context.Suburb.Add(newSuburb);
            await _context.SaveChangesAsync();
            return newSuburb;
        }

        public async Task<Suburb> EditSuburb(Suburb editedSuburb)
        {
            _context.Entry(editedSuburb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedSuburb;
        }

        public bool DeleteSuburb(Guid Id)
        {
            bool result = false;
            var suburb = _context.Suburb.Find(Id);
            if (suburb != null)
            {
                _context.Entry(suburb).State = EntityState.Deleted;
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
