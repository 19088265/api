namespace Architecture.Models
{
    public class Beneficiary
    {
        public Guid BeneficiaryId { get; internal set; } = Guid.NewGuid();
        public string BeneficiaryName { get; set; }
        public string BeneficiarySurname { get; set; }
        public string BeneficiaryIdNumber { get; set; }
        public int BeneficiaryContactNumber { get; set; }
        public string BeneficiaryAddress { get; set; }
    }
}
