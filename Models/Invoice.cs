namespace Architecture.Models
{
    public class Invoice
    {

        public Guid InvoiceId { get; internal set; } = Guid.NewGuid();
       // public Guid ItemId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int InvoiceAmount { get; set; }
        public string IsPaid { get; set; }

        // Navigation property for related InvoiceItems
        public List<ShopItem> InvoiceItems { get; set; } = new List<ShopItem>();


    }
}
