using System.ComponentModel.DataAnnotations;

namespace Architecture.Models
{
    public class Donation
    {
        public Guid DonationId { get; internal set; } = Guid.NewGuid();
        public Guid DonationTypeId { get; set; }
        public Guid SponsorId { get; set; }
        public string DonationName { get; set; }
        public string DonationDescription { get; set; }
        public DateTime DateReceived { get; set; }
        public int DonationAmount { get; set; }


    }
}
