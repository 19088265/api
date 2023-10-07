namespace Architecture.Models
{
    public class Payment
    {
        public Guid PaymentId { get; internal set; } = Guid.NewGuid();
        public Guid PaymentTypeId { get; set; }
        public Guid InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PaymentAmount { get; set; }
    }
}
