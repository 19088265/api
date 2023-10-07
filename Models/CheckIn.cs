namespace Architecture.Models
{
    public class CheckIn
    {
        public Guid CheckInId { get; internal set; } = Guid.NewGuid();
        public Guid BookId { get; set; }
        public Guid BeneficiaryId { get; set; }
        public DateTime CheckInDate { get; set; }
    }
}
