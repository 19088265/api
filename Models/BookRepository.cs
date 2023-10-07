using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class BookRepository : BookIRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Book[]> GetBooksAsync()
        {
            IQueryable<Book> query = _context.Book;
            return await query.ToArrayAsync();

        }

        public async Task<Book> GetBook(Guid Id)
        {
            IQueryable<Book> query = _context.Book;
            return await query.FirstOrDefaultAsync(x => x.BookId == Id);
        }



        //Add Book
        public async Task<Book> AddBook(Book newBook)
        {
            _context.Book.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook;
        }

        public async Task<CheckOut[]> GetCheckOutAsync()
        {
            IQueryable<CheckOut> query = _context.CheckOut;
            return await query.ToArrayAsync();

        }

        //CheckOut Book
        public async Task<CheckOut> CheckOutBook(CheckOut checkOut)
        {
            _context.CheckOut.Add(checkOut);
            await _context.SaveChangesAsync();
            return checkOut;
        }

        //CheckIn Book
        public async Task<CheckIn> CheckInBook(CheckIn checkIn)
        {
            _context.CheckIn.Add(checkIn);
            await _context.SaveChangesAsync();
            return checkIn;
        }

        //Edit Book
        public async Task<Book> EditBook(Book editedBook)
        {
            _context.Entry(editedBook).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedBook;
        }

        //Delete Book
        public bool DeleteBook(Guid Id)
        {
            bool result = false;
            var book = _context.Book.Find(Id);
            if (book != null)
            {
                _context.Entry(book).State = EntityState.Deleted;
                _context.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        //Delete Checkout
        public bool DeleteCheckout(Guid Id)
        {
            bool result = false;
            var checkout = _context.CheckOut.Find(Id);
            if (checkout != null)
            {
                _context.Entry(checkout).State = EntityState.Deleted;
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
