namespace Architecture.Models
{
    public interface PaymentIRepository
    {
        Task<Payment[]> GetPaymentAsync();
        Task<Payment> GetPayment(Guid PaymentId);

        Task<Payment> AddPayment(Payment newPayment);
        Task<Payment> EditPayment(Payment editedPayment);
        bool DeletePayment(Guid Id);
    }
}
