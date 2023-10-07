namespace Architecture.Models
{
    public class PaymentType
    {
        public Guid PaymentTypeId { get; internal set; } = Guid.NewGuid();
        public string TypeDescription { get; set; }
    }
}
