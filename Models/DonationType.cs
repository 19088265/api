namespace Architecture.Models
{
    public class DonationType
    {
        public Guid DonationTypeId { get; internal set; } = Guid.NewGuid();
        public string DonationTypeDescription { get; set; }
    }
}
