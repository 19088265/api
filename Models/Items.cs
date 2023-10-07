namespace Architecture.Models
{
    public class Items
    {
        public Guid ItemId { get; internal set; } = Guid.NewGuid();
        public string ItemName { get; set; }
        public int ItemQuantity { get; set; }
        public int ItemPrice { get; set; }
    }
}
