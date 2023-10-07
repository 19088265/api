using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class PaymentTypeRepository : PaymentTypeIRepository
    {
        private readonly AppDbContext _context;

        public PaymentTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentType[]> GetPaymentTypeAsync()
        {
            IQueryable<PaymentType> query = _context.PaymentType;
            return await query.ToArrayAsync();


        }

        public async Task<PaymentType> GetPaymentType(Guid Id)
        {
            IQueryable<PaymentType> query = _context.PaymentType;
            return await query.FirstOrDefaultAsync(x => x.PaymentTypeId == Id);
        }



        //Add PaymentType
        public async Task<PaymentType> AddPaymentType(PaymentType newPaymentType)
        {
            _context.PaymentType.Add(newPaymentType);
            await _context.SaveChangesAsync();
            return newPaymentType;
        }

        //Edit PaymentType
        public async Task<PaymentType> EditPaymentType(PaymentType editedPaymentType)
        {
            _context.Entry(editedPaymentType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedPaymentType;
        }

        //Delete PaymentType
        public bool DeletePaymentType(Guid Id)
        {


            //bool result = false;
            var paymentType = _context.PaymentType.Find(Id);
            if (paymentType == null)
            {

                return false;
            }

            // Check if there are any payments referencing this PaymentType
            bool hasReferencedPayments = _context.Payment.Any(s => s.PaymentTypeId == Id);

            if (hasReferencedPayments)
            {
                // There are referenced payments, so we cannot delete the PaymentType
                return false;
            }

            try
            {
                _context.PaymentType.Remove(paymentType);
                _context.SaveChanges();
                return true; // Deletion successful
            }
            catch (Exception)
            {
                // Handle any exception that may occur during deletion
                return false; // Deletion failed
            }
        }
    }
}
