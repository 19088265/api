using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class SponsorTypeRepository : SponsorTypeIRepository
    {
        private readonly AppDbContext _context;

        public SponsorTypeRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<SponsorType> GetSponsorType(Guid Id)
        {
            IQueryable<SponsorType> query = _context.SponsorType;
            return await query.FirstOrDefaultAsync(x => x.SponsorTypeId == Id);
        }
    }
}
