namespace Architecture.Models
{
    public interface BookGenreIRepository
    {
        Task<BookGenre[]> GetBookGenreAsync();
        Task<BookGenre> GetBookGenre(Guid BookGenreId);

        Task<BookGenre> AddBookGenre(BookGenre newGenre);
        Task<BookGenre> EditBookGenre(BookGenre editedGenre);
        bool DeleteBookGenre(Guid Id);
    }
}
