namespace Architecture.Models
{
    public interface InvoiceIRepository
    {
        Task<Invoice[]> GetInvoiceAsync();
        Task<Invoice> GetInvoice(Guid InvoiceId);

        Task<Invoice> AddInvoice(Invoice newInvoice);
        Task<Invoice> EditInvoice(Invoice editedInvoice);
        Task <bool> DeleteInvoice(Guid Id);

        // Methods for handling InvoiceItems
        Task<ShopItem[]> GetInvoiceItems(Guid invoiceId);
        Task<ShopItem> AddInvoiceItem(Guid invoiceId, ShopItem newInvoiceItem);
        Task<ShopItem> EditInvoiceItem(ShopItem editedInvoiceItem);
        bool DeleteInvoiceItem(Guid invoiceItemId);
    }
}
