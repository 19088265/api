namespace Architecture.Models
{
    public class Sponsor
    {
        public Guid SponsorId { get; internal set; } = Guid.NewGuid();
        public Guid SponsorTypeId { get; set; }
        public string SponsorName { get; set; }
        public string SponsorEmail { get; set; }
        public string SponsorContactNumber { get; set; }

        //ForeignKey
        //public SponsorType SponsorType { get; set; }

    }
}
