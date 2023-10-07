namespace Architecture.Models
{
    public class BookStatus
    {
        public Guid BookStatusId { get; internal set; } = Guid.NewGuid();
        public string BookDescription { get; set; }
    }
}
