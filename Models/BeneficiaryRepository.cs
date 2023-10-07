using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class BeneficiaryRepository : BeneficiaryIRepository
    {
        private readonly AppDbContext _context;

        public BeneficiaryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Beneficiary[]> GetBeneficiaryAsync()
        {
            IQueryable<Beneficiary> query = _context.Beneficiary;
            return await query.ToArrayAsync();
        }

        public async Task<Beneficiary> GetBeneficiary(Guid Id)
        {
            IQueryable<Beneficiary> query = _context.Beneficiary;
            return await query.FirstOrDefaultAsync(x => x.BeneficiaryId == Id);
        }


        //Add Beneficiary
        public async Task<Beneficiary> AddBeneficiary(Beneficiary newBene)
        {
            _context.Beneficiary.Add(newBene);
            await _context.SaveChangesAsync();
            return newBene;
        }
        //Update Beneficiary
        public async Task<Beneficiary> EditBeneficiary(Beneficiary editBene)
        {
            _context.Entry(editBene).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editBene;
        }
        //Delete Beneficiary

        public bool DeleteBeneficiary(Guid Id)
        {



            var bene = _context.Beneficiary.Find(Id);
            if (bene == null)
            {
                //_context.Entry(sponsorType).State = EntityState.Deleted;
                //_context.SaveChanges();
                return false;
            }

            // Check if there are any books referencing this BookStatus
            bool hasReferencedBooks = _context.CheckOut.Any(s => s.BeneficiaryId == Id);

            if (hasReferencedBooks)
            {
                // There are referenced books, so we cannot delete the status
                return false;
            }

            try
            {
                _context.Beneficiary.Remove(bene);
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
