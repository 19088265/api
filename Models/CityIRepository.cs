namespace Architecture.Models
{
    public interface CityIRepository
    {
        Task<City[]> GetCityAsync();
        Task<City> GetCity(Guid CityId);
        // Task<Sponsor> GetSponsorsBySponsorTypeId(Guid sponsorTypeId);

        Task<City> AddCity(City city);
        Task<City> EditCity(City editedSponsor);
        bool DeleteCity(Guid Id);
    }
}
