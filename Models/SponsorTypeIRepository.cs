namespace Architecture.Models
{
    public interface SponsorTypeIRepository
    {
        Task<SponsorType> GetSponsorType(Guid SponsorTypeId);
    }
}
