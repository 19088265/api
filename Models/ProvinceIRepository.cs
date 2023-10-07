namespace Architecture.Models
{
    public interface ProvinceIRepository
    {
        Task<Province[]> GetProvinceAsync();
        Task<Province> GetProvince(Guid ProvinceId);

        Task<Province> AddProvince(Province newProvince);
        Task<Province> EditProvince(Province editedProvince);
        bool DeleteProvince(Guid Id);
    }
}
