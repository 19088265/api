namespace Architecture.Models
{
    public interface BeneficiaryIRepository
    {
        //Beneficiary
        Task<Beneficiary[]> GetBeneficiaryAsync();
        Task<Beneficiary> GetBeneficiary(Guid BeneficiaryId);
        Task<Beneficiary> AddBeneficiary(Beneficiary newBene);
        Task<Beneficiary> EditBeneficiary(Beneficiary editBene);
        bool DeleteBeneficiary(Guid Id);
    }
}
