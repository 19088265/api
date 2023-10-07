using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class DonationTypeRepository : DonationTypeIRepository
    {
        private readonly AppDbContext _context;

        public DonationTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DonationType[]> GetDonationTypeAsync()
        {
            IQueryable<DonationType> query = _context.DonationType;
            return await query.ToArrayAsync();


        }

        public async Task<DonationType> GetDonationType(Guid Id)
        {
            IQueryable<DonationType> query = _context.DonationType;
            return await query.FirstOrDefaultAsync(x => x.DonationTypeId == Id);
        }



        //Add DonationType
        public async Task<DonationType> AddDonationType(DonationType newDonationType)
        {
            _context.DonationType.Add(newDonationType);
            await _context.SaveChangesAsync();
            return newDonationType;
        }

        //Edit DonationType
        public async Task<DonationType> EditDonationType(DonationType editedDonationType)
        {
            _context.Entry(editedDonationType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedDonationType;
        }

        //Delete DonationType
        public bool DeleteDonationType(Guid Id)
        {


            //bool result = false;
            var donationType = _context.DonationType.Find(Id);
            if (donationType == null)
            {
                //_context.Entry(sponsorType).State = EntityState.Deleted;
                //_context.SaveChanges();
                return false;
            }

            // Check if there are any donations referencing this DonationType
            bool hasReferencedDonations = _context.Donation.Any(s => s.DonationTypeId == Id);

            if (hasReferencedDonations)
            {
                // There are referenced donations, so we cannot delete the DonationType
                return false;
            }

            try
            {
                _context.DonationType.Remove(donationType);
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
