namespace Architecture.Models
{
    public class Book
    {
        public Guid BookId { get; internal set; } = Guid.NewGuid();
        public Guid BookGenreId { get; set; }
        public Guid BookStatusId { get; set; }
        public string BookAuthor { get; set; }
        public string BookName { get; set; }
        public string Isbn { get; set; }
    }
}
