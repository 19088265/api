namespace Architecture.Models
{
    public class Suburb
    {
        public Guid SuburbId { get; set; }
        public Guid CityId { get; set; }
        public string SuburbName { get; set; }
    }
}
