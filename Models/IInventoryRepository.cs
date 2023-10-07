using Architecture.Dtos;

namespace Architecture.Models
{
    public interface IInventoryRepository
    {
        // Inventory
        Task<Inventory[]> GetAllInventoryAsync();
        Task<Inventory> GetInventoryAsync(Guid inventoryId);
        Task<Inventory> AddInventory(Inventory newInventory);
        Task<Inventory> EditInventory(Inventory editedInventory);
        bool DeleteInventory(Guid ID);



        // Inventory Type
        Task<InventoryType[]> GetInventoryTypeAsync();
        Task<InventoryType> GetInventoryTypeAsync(Guid inventoryTypeId);
        Task<InventoryType> AddInventoryType(InventoryType newInventoryType);
        Task<InventoryType> EditInventoryType(InventoryType editedInventoryType);
        bool DeleteInventoryType(Guid ID);

        Task<IEnumerable<InventoryTypeQuantityDto>> GetInventoryQuantitiesPerTypeAsync();
    }
}
