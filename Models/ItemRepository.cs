using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class ItemRepository : ItemIRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Items[]> GetItemAsync()
        {
            IQueryable<Items> query = _context.Items;
            return await query.ToArrayAsync();


        }

        public async Task<Items> GetItem(Guid Id)
        {
            IQueryable<Items> query = _context.Items;
            return await query.FirstOrDefaultAsync(x => x.ItemId == Id);
        }



        //Add Item
        public async Task<Items> AddItem(Items newItem)
        {
            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
            return newItem;
        }

        //Edit Item
        public async Task<Items> EditItem(Items editedItem)
        {
            _context.Entry(editedItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return editedItem;
        }

        //Delete Item
        public bool DeleteItem(Guid Id)
        {


            //bool result = false;
            var item = _context.Items.Find(Id);
            if (item == null)
            {

                return false;
            }

            // Check if there are any payments referencing this PaymentType
            //bool hasReferencedInvoices = _context.Invoice.Any(s => s.ItemId == Id);

            //if (hasReferencedInvoices)
            //{
                // There are referenced payments, so we cannot delete the PaymentType
                //return false;
            //}

            try
            {
                _context.Items.Remove(item);
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
