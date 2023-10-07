namespace Architecture.Models
{
    public interface SuburbIRepository
    {
        Task<Suburb[]> GetSuburbAsync();
        Task<Suburb> GetSuburb(Guid SuburbId);


        Task<Suburb> AddSuburb(Suburb suburb);
        Task<Suburb> EditSuburb(Suburb editedSuburb);
        bool DeleteSuburb(Guid Id);
    }
}
