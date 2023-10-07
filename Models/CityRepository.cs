using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class CityRepository : CityIRepository
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<City[]> GetCityAsync()
        {
            IQueryable<City> query = _context.City;
            return await query.ToArrayAsync();
        }

        public async Task<City> GetCity(Guid Id)
        {
            IQueryable<City> query = _context.City;
            return await query.FirstOrDefaultAsync(x => x.CityId == Id);
        }

        public async Task<City> AddCity(City newCity)
        {
            _context.City.Add(newCity);
            await _context.SaveChangesAsync();
            return newCity;
        }

        public async Task<City> EditCity(City editedCity)
        {
            _context.Entry(editedCity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedCity;
        }

        public bool DeleteCity(Guid Id)
        {



            var city = _context.City.Find(Id);
            if (city == null)
            {

                return false;
            }

            // Check if there are any suburbs referencing this City
            bool hasReferencedSuburbs = _context.Suburb.Any(s => s.CityId == Id);

            if (hasReferencedSuburbs)
            {
                // There are referenced suburbs, so we cannot delete the city
                return false;
            }

            try
            {
                _context.City.Remove(city);
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
