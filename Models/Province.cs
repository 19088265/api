namespace Architecture.Models
{
    public class Province
    {
        public Guid ProvinceId { get; internal set; } = Guid.NewGuid();


        public string ProvinceName { get; set; }

        //public List<City> City { get; set; }
    }
}
