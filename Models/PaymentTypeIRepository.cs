namespace Architecture.Models
{
    public interface PaymentTypeIRepository
    {
        Task<PaymentType[]> GetPaymentTypeAsync();
        Task<PaymentType> GetPaymentType(Guid PaymentTypeId);

        Task<PaymentType> AddPaymentType(PaymentType newPaymentType);
        Task<PaymentType> EditPaymentType(PaymentType editedPaymentType);
        bool DeletePaymentType(Guid Id);
    }
}
