namespace Architecture.Models
{
    public class SponsorType
    {
        public Guid SponsorTypeId { get; internal set; } = Guid.NewGuid();
        public string SponsorTypeDescription { get; set; }

        //public List<Sponsor> Sponsors { get; set; }
    }
}
