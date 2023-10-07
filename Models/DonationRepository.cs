using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class DonationRepository : DonationIRepository
    {
        private readonly AppDbContext _context;

        public DonationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Donation[]> GetDonationAsync()
        {
            IQueryable<Donation> query = _context.Donation;
            return await query.ToArrayAsync();
        }

        public async Task<Donation> GetDonation(Guid Id)
        {
            IQueryable<Donation> query = _context.Donation;
            return await query.FirstOrDefaultAsync(x => x.DonationId == Id);
        }

        public async Task<Donation> AddDonation(Donation newDonation)
        {
            _context.Donation.Add(newDonation);
            await _context.SaveChangesAsync();
            return newDonation;
        }

        public async Task<Donation> EditDonation(Donation editedDonation)
        {
            _context.Entry(editedDonation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedDonation;
        }

        public bool DeleteDonation(Guid Id)
        {
            bool result = false;
            var donation = _context.Donation.Find(Id);
            if (donation != null)
            {
                _context.Entry(donation).State = EntityState.Deleted;
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
