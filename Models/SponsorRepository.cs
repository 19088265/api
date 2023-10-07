using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class SponsorRepository : SponsorIRepository
    {
        private readonly AppDbContext _context;

        public SponsorRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Sponsor[]> GetSponsorsAsync()
        {
            IQueryable<Sponsor> query = _context.Sponsor;
            return await query.ToArrayAsync();
        }

        public async Task<Sponsor> GetSponsor(Guid Id)
        {
            IQueryable<Sponsor> query = _context.Sponsor;
            return await query.FirstOrDefaultAsync(x => x.SponsorId == Id);
        }

        public async Task<Sponsor> GetSponsorsBySponsorTypeId(Guid sponsorTypeId)
        {
            IQueryable<Sponsor> query = _context.Sponsor;
            return await query.FirstOrDefaultAsync(s => s.SponsorTypeId == sponsorTypeId);
        }

        //Add Sponsor
        public async Task<Sponsor> AddSponsor(Sponsor newSponsor)
        {
            _context.Sponsor.Add(newSponsor);
            await _context.SaveChangesAsync();
            return newSponsor;
        }

        //Update Sponsor
        public async Task<Sponsor> EditSponsor(Sponsor editedSponsor)
        {
            _context.Entry(editedSponsor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedSponsor;
        }

        //Delete Sponsor
        public bool DeleteSponsor(Guid Id)
        {
            //bool result = false;
            var sponsor = _context.Sponsor.Find(Id);
            if (sponsor == null)
            {
                // _context.Entry(sponsor).State = EntityState.Deleted;
                // _context.SaveChanges();
                return false;
            }
            bool hasReferencedDonations = _context.Donation.Any(s => s.SponsorId == Id);

            if (hasReferencedDonations)
            {
                // There are referenced donations, so we cannot delete the Sponsor
                return false;
            }

            try
            {
                _context.Sponsor.Remove(sponsor);
                _context.SaveChanges();
                return true; // Deletion successful
            }
            catch (Exception)
            {
                // Handle any exception that may occur during deletion
                return false; // Deletion failed
            }

        }



        /////InventoryType//////

        public async Task<SponsorType[]> GetSponsorTypeAsync()
        {
            IQueryable<SponsorType> query = _context.SponsorType;
            return await query.ToArrayAsync();

            //Include(st => st.Sponsors)
        }



        //Add SponsorType
        public async Task<SponsorType> AddSponsorType(SponsorType newSponsorType)
        {
            _context.SponsorType.Add(newSponsorType);
            await _context.SaveChangesAsync();
            return newSponsorType;
        }

        //Edit SponsorType
        public async Task<SponsorType> EditSponsorType(SponsorType editedSponsorType)
        {
            _context.Entry(editedSponsorType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedSponsorType;
        }

        //Delete SponsorType
        public bool DeleteSponsorType(Guid Id)
        {


            //bool result = false;
            var sponsorType = _context.SponsorType.Find(Id);
            if (sponsorType == null)
            {
                //_context.Entry(sponsorType).State = EntityState.Deleted;
                //_context.SaveChanges();
                return false;
            }

            // Check if there are any sponsors referencing this SponsorType
            bool hasReferencedSponsors = _context.Sponsor.Any(s => s.SponsorTypeId == Id);

            if (hasReferencedSponsors)
            {
                // There are referenced sponsors, so we cannot delete the SponsorType
                return false;
            }

            try
            {
                _context.SponsorType.Remove(sponsorType);
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
