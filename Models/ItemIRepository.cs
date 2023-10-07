namespace Architecture.Models
{
    public interface ItemIRepository
    {
        Task<Items[]> GetItemAsync();
        Task<Items> GetItem(Guid ItemId);

        Task<Items> AddItem(Items newItem);
        Task<Items> EditItem(Items editedItem);
        bool DeleteItem(Guid Id);
    }
}
