namespace Architecture.Models
{
    public interface DonationIRepository
    {
        Task<Donation[]> GetDonationAsync();
        Task<Donation> GetDonation(Guid DonationId);

        Task<Donation> AddDonation(Donation newDonation);
        Task<Donation> EditDonation(Donation editedDonation);
        bool DeleteDonation(Guid Id);
    }
}
