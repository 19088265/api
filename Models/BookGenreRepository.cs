using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class BookGenreRepository : BookGenreIRepository
    {
        private readonly AppDbContext _context;

        public BookGenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookGenre[]> GetBookGenreAsync()
        {
            IQueryable<BookGenre> query = _context.BookGenre;
            return await query.ToArrayAsync();

        }

        public async Task<BookGenre> GetBookGenre(Guid Id)
        {
            IQueryable<BookGenre> query = _context.BookGenre;
            return await query.FirstOrDefaultAsync(x => x.BookGenreId == Id);
        }



        //Add BookGenre
        public async Task<BookGenre> AddBookGenre(BookGenre newGenre)
        {
            _context.BookGenre.Add(newGenre);
            await _context.SaveChangesAsync();
            return newGenre;
        }

        //Edit BookGenre
        public async Task<BookGenre> EditBookGenre(BookGenre editedGenre)
        {
            _context.Entry(editedGenre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedGenre;
        }

        //Delete BookGenre
        public bool DeleteBookGenre(Guid Id)
        {


            //bool result = false;
            var genre = _context.BookGenre.Find(Id);
            if (genre == null)
            {
                //_context.Entry(sponsorType).State = EntityState.Deleted;
                //_context.SaveChanges();
                return false;
            }

            // Check if there are any books referencing this BookGenre
            bool hasReferencedBooks = _context.Book.Any(s => s.BookGenreId == Id);

            if (hasReferencedBooks)
            {
                // There are referenced books, so we cannot delete the genre
                return false;
            }

            try
            {
                _context.BookGenre.Remove(genre);
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
