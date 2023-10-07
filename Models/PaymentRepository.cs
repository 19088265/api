using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class PaymentRepository : PaymentIRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Payment[]> GetPaymentAsync()
        {
            IQueryable<Payment> query = _context.Payment;
            return await query.ToArrayAsync();


        }

        public async Task<Payment> GetPayment(Guid Id)
        {
            IQueryable<Payment> query = _context.Payment;
            return await query.FirstOrDefaultAsync(x => x.PaymentId == Id);
        }



        //Add PaymentType
        public async Task<Payment> AddPayment(Payment newPayment)
        {
            _context.Payment.Add(newPayment);
            await _context.SaveChangesAsync();

            string invoiceId = newPayment.InvoiceId.ToString();
            UpdateInvoiceStatus(invoiceId, "Paid");
            return newPayment;


        }

        private void UpdateInvoiceStatus(string invoiceId, string status)
        {


            var invoice = _context.Invoice.FirstOrDefault(i => i.InvoiceId == new Guid(invoiceId));

            if (invoice != null)
            {
                invoice.IsPaid = status;
                _context.SaveChanges();
            }
        }

        //Edit PaymentType
        public async Task<Payment> EditPayment(Payment editedPayment)
        {
            _context.Entry(editedPayment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedPayment;
        }

        //Delete PaymentType
        public bool DeletePayment(Guid Id)
        {

            bool result = false;
            var payment = _context.Payment.Find(Id);
            if (payment != null)
            {
                _context.Entry(payment).State = EntityState.Deleted;
                _context.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;


        }
    }
}
