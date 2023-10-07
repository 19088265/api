namespace Architecture.Models
{
    public interface CafeteriaIRepository
    {
        //Cafeteria
        Task<Cafeteria[]> GetCafeteriaAsync();
        Task<Cafeteria> GetCafeteria(Guid CafeId);
        Task<Cafeteria> AddCafeteria(Cafeteria newBene);
        Task<Cafeteria> EditCafeteria(Cafeteria editBene);
        bool DeleteCafeteria(Guid Id);
    }
}
