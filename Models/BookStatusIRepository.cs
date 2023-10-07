namespace Architecture.Models
{
    public interface BookStatusIRepository
    {
        Task<BookStatus[]> GetBookStatusAsync();
        Task<BookStatus> GetBookStatus(Guid BookStatusId);

        Task<BookStatus> AddBookStatus(BookStatus newStatus);
        Task<BookStatus> EditBookStatus(BookStatus editedStatus);
        bool DeleteBookStatus(Guid Id);
    }
}
