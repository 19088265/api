using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
        public class BookStatusRepository : BookStatusIRepository
        {
            private readonly AppDbContext _context;

            public BookStatusRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<BookStatus[]> GetBookStatusAsync()
            {
                IQueryable<BookStatus> query = _context.BookStatus;
                return await query.ToArrayAsync();

            }

            public async Task<BookStatus> GetBookStatus(Guid Id)
            {
                IQueryable<BookStatus> query = _context.BookStatus;
                return await query.FirstOrDefaultAsync(x => x.BookStatusId == Id);
            }



            //Add BookStatus
            public async Task<BookStatus> AddBookStatus(BookStatus newStatus)
            {
                _context.BookStatus.Add(newStatus);
                await _context.SaveChangesAsync();
                return newStatus;
            }

            //Edit BookStatus
            public async Task<BookStatus> EditBookStatus(BookStatus editedStatus)
            {
                _context.Entry(editedStatus).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return editedStatus;
            }

            //Delete BookStatus
            public bool DeleteBookStatus(Guid Id)
            {



                var status = _context.BookStatus.Find(Id);
                if (status == null)
                {
                    //_context.Entry(sponsorType).State = EntityState.Deleted;
                    //_context.SaveChanges();
                    return false;
                }

                // Check if there are any books referencing this BookStatus
                bool hasReferencedBooks = _context.Book.Any(s => s.BookStatusId == Id);

                if (hasReferencedBooks)
                {
                    // There are referenced books, so we cannot delete the status
                    return false;
                }

                try
                {
                    _context.BookStatus.Remove(status);
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
