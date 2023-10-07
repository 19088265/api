namespace Architecture.Models
{
    public class ShopItem
    {
        public Guid ShopItemId { get; internal set; } = Guid.NewGuid();
        public Guid InvoiceId { get; set; }
        public string ItemName { get; set; }
        public int ItemQuantity { get; set; }
        public int ItemPrice { get; set; }

        // Navigation property to the parent Invoice
        //public Invoice Invoice { get; set; }
    }
}
