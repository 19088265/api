namespace Architecture.Models
{
    public class CheckOut
    {

        public Guid CheckOutId { get; internal set; } = Guid.NewGuid();
        public Guid BookId { get; set; }
        public Guid BeneficiaryId { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
