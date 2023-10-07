namespace Architecture.Models
{
    public interface SponsorIRepository
    {
        //Sponsor
        Task<Sponsor[]> GetSponsorsAsync();
        Task<Sponsor> GetSponsor(Guid SponsorId);
        Task<Sponsor> GetSponsorsBySponsorTypeId(Guid sponsorTypeId);

        Task<Sponsor> AddSponsor(Sponsor sponsor);
        Task<Sponsor> EditSponsor(Sponsor editedSponsor);
        bool DeleteSponsor(Guid Id);

        //SponsorType
        Task<SponsorType[]> GetSponsorTypeAsync();

        Task<SponsorType> AddSponsorType(SponsorType newSponsorType);
        Task<SponsorType> EditSponsorType(SponsorType editedSponsorType);
        bool DeleteSponsorType(Guid Id);
    }
}
