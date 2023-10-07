namespace Architecture.Models
{
    public interface CafeteriaTypeIRepository
    {
        Task<CafeteriaType[]> GetCafeteriaTypeAsync();

        Task<CafeteriaType> GetCafeteriaType(Guid CafeteriaTypeId);

        Task<CafeteriaType> AddCafeteriaType(CafeteriaType newCafeType);
        Task<CafeteriaType> EditCafeteriaType(CafeteriaType editedCafeType);
        bool DeleteCafeteriaType(Guid Id);
    }
}
