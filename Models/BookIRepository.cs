namespace Architecture.Models
{
    public interface BookIRepository
    {
        Task<Book[]> GetBooksAsync();
        Task<Book> GetBook(Guid BookId);

        Task<Book> AddBook(Book book);
        Task<Book> EditBook(Book editedBook);
        bool DeleteBook(Guid Id);

        Task<CheckOut[]> GetCheckOutAsync();

        Task<CheckOut> CheckOutBook(CheckOut checkOut);

        Task<CheckIn> CheckInBook(CheckIn checkin);

        bool DeleteCheckout(Guid Id);
    }
}
