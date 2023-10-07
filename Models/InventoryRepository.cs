using Architecture.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public InventoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Inventory[]> GetAllInventoryAsync()
        {
            IQueryable<Inventory> query = _appDbContext.Inventory.Include(e => e.InventoryType); ;
            return await query.ToArrayAsync();
        }

        //Get one record
        public async Task<Inventory> GetInventoryAsync(Guid inventoryId)
        {
            IQueryable<Inventory> query = _appDbContext.Inventory.Where(c => c.InventoryId == inventoryId).Include(e => e.InventoryType);
            return await query.FirstOrDefaultAsync();
        }

        //Add inventory
        public async Task<Inventory> AddInventory(Inventory newInventory)
        {
            _appDbContext.Inventory.Add(newInventory);
            await _appDbContext.SaveChangesAsync();
            return newInventory;
        }

        //Update inventory 
        public async Task<Inventory> EditInventory(Inventory editedInventory)
        {
            _appDbContext.Entry(editedInventory).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedInventory;
        }

        public bool DeleteInventory(Guid ID)
        {
            bool result = false;
            //var employeeType = _appDbContext.EmployeeType.Find(ID);
            var inventory = _appDbContext.Inventory.FirstOrDefault(p => p.InventoryId == ID);
            if (inventory != null)
            {
                //_appDbContext.Entry(employeeType).State = EntityState.Deleted;
                _appDbContext.Inventory.Remove(inventory);
                _appDbContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        //////////////////////////////////////////////////////////////////////////////
        // Inventory type

        public async Task<InventoryType[]> GetInventoryTypeAsync()
        {
            IQueryable<InventoryType> query = _appDbContext.InventoryType;
            return await query.ToArrayAsync();
        }

        //Get one record
        public async Task<InventoryType> GetInventoryTypeAsync(Guid inventoryTypeId)
        {
            IQueryable<InventoryType> query = _appDbContext.InventoryType.Where(c => c.InventoryTypeId == inventoryTypeId);
            return await query.FirstOrDefaultAsync();
        }


        //Add inventory type
        public async Task<InventoryType> AddInventoryType(InventoryType newInventoryType)
        {
            _appDbContext.InventoryType.Add(newInventoryType);
            await _appDbContext.SaveChangesAsync();
            return newInventoryType;
        }

        //Update inventory type
        public async Task<InventoryType> EditInventoryType(InventoryType editedInventoryType)
        {
            _appDbContext.Entry(editedInventoryType).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedInventoryType;
        }

        public bool DeleteInventoryType(Guid ID)
        {
            bool result = false;
            var studentType = _appDbContext.InventoryType.FirstOrDefault(p => p.InventoryTypeId == ID);
            if (studentType != null)
            {
                // Check if the type is referenced in the other table
                var isReferencedInStudent = _appDbContext.Inventory.Any(e => e.InventoryTypeId == ID);

                if (isReferencedInStudent)
                {
                    // If referenced, do not delete and return false
                    return false;
                }
                else
                {
                    _appDbContext.InventoryType.Remove(studentType);
                    _appDbContext.SaveChanges();
                    return true;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        ///////////////////////////////////////////////////
        public async Task<IEnumerable<InventoryTypeQuantityDto>> GetInventoryQuantitiesPerTypeAsync()
        {
            var inventoryQuantities = await _appDbContext.Inventory
                .GroupBy(i => i.InventoryTypeId)
                .Select(g => new InventoryTypeQuantityDto
                {
                    InventoryTypeDescription = g.FirstOrDefault().InventoryType.InventoryTypeDescription,
                    Quantity = g.Sum(i => i.QuantityOnHand)
                })
                .ToListAsync();

            return inventoryQuantities;
        }
    }
}
