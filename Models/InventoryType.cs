namespace Architecture.Models
{
    public class InventoryType
    {

        public Guid InventoryTypeId { get; internal set; } = Guid.NewGuid();
        public string InventoryTypeDescription { get; set; }

    }
}
