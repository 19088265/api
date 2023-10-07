namespace Architecture.Models
{
    public class City
    {
        public Guid CityId { get; internal set; } = Guid.NewGuid();
        public Guid ProvinceId { get; set; }
        public string CityName { get; set; }

        //public Province Province { get; set; }
    }
}
