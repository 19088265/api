namespace Architecture.Models
{
    public interface DonationTypeIRepository
    {
        Task<DonationType[]> GetDonationTypeAsync();
        Task<DonationType> GetDonationType(Guid DonationTypeId);

        Task<DonationType> AddDonationType(DonationType newDonationType);
        Task<DonationType> EditDonationType(DonationType editedDonationType);
        bool DeleteDonationType(Guid Id);
    }
}
