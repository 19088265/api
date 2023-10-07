using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class ProvinceRepository : ProvinceIRepository
    {
        private readonly AppDbContext _context;

        public ProvinceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Province[]> GetProvinceAsync()
        {
            IQueryable<Province> query = _context.Province;
            return await query.ToArrayAsync();

            //Include(st => st.Sponsors)
        }

        public async Task<Province> GetProvince(Guid Id)
        {
            IQueryable<Province> query = _context.Province;
            return await query.FirstOrDefaultAsync(x => x.ProvinceId == Id);
        }



        //Add Province
        public async Task<Province> AddProvince(Province newProvince)
        {
            _context.Province.Add(newProvince);
            await _context.SaveChangesAsync();
            return newProvince;
        }

        //Edit Province
        public async Task<Province> EditProvince(Province editedProvince)
        {
            _context.Entry(editedProvince).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedProvince;
        }

        //Delete Province
        public bool DeleteProvince(Guid Id)
        {


            //bool result = false;
            var province = _context.Province.Find(Id);
            if (province == null)
            {
                //_context.Entry(sponsorType).State = EntityState.Deleted;
                //_context.SaveChanges();
                return false;
            }

            // Check if there are any cities referencing this Province
            bool hasReferencedSponsors = _context.City.Any(s => s.ProvinceId == Id);

            if (hasReferencedSponsors)
            {
                // There are referenced cities, so we cannot delete the province
                return false;
            }

            try
            {
                _context.Province.Remove(province);
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
