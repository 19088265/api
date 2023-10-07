using System.ComponentModel.DataAnnotations;

namespace Architecture.Models
{
    public class Inventory
    {
        [Key]
        public Guid InventoryId { get; internal set; } = Guid.NewGuid();
        public string InventoryName { get; set; }
        public string InventoryDescription { get; set; }
        public int QuantityOnHand { get; set; }

        //[ForeignKey("InventoryTypeId")]
        //public virtual InventoryType InventoryType { get; set; }
        public Guid InventoryTypeId { get; set; }
        public InventoryType InventoryType { get; set; }
    }
}
