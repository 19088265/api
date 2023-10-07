using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class InvoiceRepository : InvoiceIRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice[]> GetInvoiceAsync()
        {
            // Include related InvoiceItems
            IQueryable<Invoice> query = _context.Invoice.Include(i => i.InvoiceItems);
            return await query.ToArrayAsync();
        }

        public async Task<Invoice> GetInvoice(Guid id)
        {
            // Include related InvoiceItems
            IQueryable<Invoice> query = _context.Invoice.Include(i => i.InvoiceItems);
            return await query.FirstOrDefaultAsync(x => x.InvoiceId == id);
        }

        // Add Invoice with associated InvoiceItems
        public async Task<Invoice> AddInvoice(Invoice newInvoice)
        {
            _context.Invoice.Add(newInvoice);
            await _context.SaveChangesAsync();
            return newInvoice;
        }

        // Edit Invoice (and associated InvoiceItems)
        public async Task<Invoice> EditInvoice(Invoice editedInvoice)
        {
            _context.Entry(editedInvoice).State = EntityState.Modified;

            // Assuming InvoiceItems are also modified with the edited Invoice
            foreach (var item in editedInvoice.InvoiceItems)
            {
                _context.Entry(item).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return editedInvoice;
        }

        // Delete Invoice (and associated InvoiceItems)
        public async Task<bool> DeleteInvoice(Guid id)
        {
            bool result = false;

            // Use FirstOrDefaultAsync to get the result asynchronously
            var invoice = await _context.Invoice.Include(i => i.InvoiceItems).FirstOrDefaultAsync(x => x.InvoiceId == id);

            if (invoice != null)
            {
                // Delete associated InvoiceItems
                _context.ShopItem.RemoveRange(invoice.InvoiceItems);

                // Use async SaveChangesAsync and await it
                await _context.SaveChangesAsync();

                // Mark the invoice for deletion
                _context.Entry(invoice).State = EntityState.Deleted;

                // Use async SaveChangesAsync and await it
                await _context.SaveChangesAsync();

                result = true;
            }

            return result;
        }


        //Invoice items
        public async Task<ShopItem[]> GetInvoiceItemsa()
        {
            // Include related InvoiceItems
            IQueryable<ShopItem> query = _context.ShopItem;
            return await query.ToArrayAsync();
        }

        public async Task<ShopItem[]> GetInvoiceItems(Guid invoiceId)
        {
            // Include related InvoiceItems
            IQueryable<ShopItem> query = _context.ShopItem;
            return await query.ToArrayAsync(); ;
        }

        // Add Invoice with associated InvoiceItems
        public async Task<ShopItem> AddInvoiceItem(Guid invoiceId, ShopItem newShopItem)
        {
            // Assuming ShopItem has an InvoiceId property
            newShopItem.InvoiceId = invoiceId;

            _context.ShopItem.Add(newShopItem);
            await _context.SaveChangesAsync();
            return newShopItem;
        }

        // Edit Invoice (and associated InvoiceItems)
        public async Task<ShopItem> EditInvoiceItem(ShopItem editedInvoiceItem)
        {
            _context.Entry(editedInvoiceItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedInvoiceItem;
        }

        // Delete Invoice (and associated InvoiceItems)
        public bool DeleteInvoiceItem(Guid id)
        {
            bool result = false;

            var invoiceItem = _context.ShopItem.FirstOrDefaultAsync(x => x.ShopItemId == id);

            if (invoiceItem != null)
            {
                _context.Entry(invoiceItem).State = EntityState.Deleted;
                _context.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}

